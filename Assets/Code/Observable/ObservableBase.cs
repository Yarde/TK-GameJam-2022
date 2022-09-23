using System;
using System.Collections.Generic;
using UnityEngine;

namespace Yarde.Observable
{
    public abstract class ObservableBase
    {
        private readonly ObservableBase _parent;
        private bool _invokeOnUnlock;
        private bool _isDuringUnlocking;
        private int _lockCount;
        protected int ChangedChildrenCount;
        protected List<ObservableBase> Children;

        protected ObservableBase(ObservableBase parent = null)
        {
            _parent = parent;
            _parent?.AddChild(this);
        }

        private bool IsLocked
        {
            get
            {
                var current = this;
                while (current != null)
                {
                    if (current._lockCount > 0) return true;
                    current = current._parent;
                }

                return false;
            }
        }

        private event Action OnChangedInternal;

        private void AddChild(ObservableBase child)
        {
            Children ??= new List<ObservableBase>();
            Children.Add(child);

            child.OnChangedInternal += OnChildUpdated;
        }

        private void OnChildUpdated()
        {
            if (_isDuringUnlocking)
                _invokeOnUnlock = true;
            else
                SignalChange();
        }

        protected void SignalChange()
        {
            if (IsLocked)
                _invokeOnUnlock = true;
            else
                InvokeChange();
        }

        protected virtual void InvokeChange()
        {
            OnChangedInternal?.Invoke();
        }

        /// <summary>
        /// Increases the lock count effectively blocking the Observable from Invoking the events unlit consistent
        /// </summary>
        /// <param name="reporter">Optional parameter used for debug, help to keep track of all locks</param>
        public void IncreaseLock(Type reporter = null)
        {
            _lockCount++;
            ChangedChildrenCount = 0;
            Debug.Log($"[ObservableSystem] added a new lock by: {reporter}, current locks: {_lockCount}");
        }

        /// <summary>
        /// Decreases the lock could, if lock count goes down to 0 it unlocks the Observer and invokes OnChange events
        /// </summary>
        /// <param name="reporter">Optional parameter used for debug, help to keep track of all locks</param>
        public void DecreaseLock(Type reporter = null)
        {
            _lockCount--;
            if (!IsLocked) OnUnlock();
            Debug.Log($"[ObservableSystem] removed a lock by: {reporter}, current locks: {_lockCount}");
        }

        private void OnUnlock()
        {
            _isDuringUnlocking = true;

            if (Children != null)
                foreach (var child in Children)
                {
                    if (child._invokeOnUnlock) ChangedChildrenCount++;

                    child.OnUnlock();
                }

            if (_invokeOnUnlock)
            {
                InvokeChange();
                _invokeOnUnlock = false;
            }

            _isDuringUnlocking = false;
        }
    }
}