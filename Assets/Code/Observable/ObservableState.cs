using System;

namespace Yarde.Observable
{
    public abstract class ObservableState : ObservableBase, IObservableState
    {
        public Action<ObservableState> OnStateChanged { get; set; }
        public Action<ObservableState> OnAllChildrenChanged { get; set; }

        protected override void InvokeChange()
        {
            OnStateChanged?.Invoke(this);
            TrySignalAllChanged();
            base.InvokeChange();
        }

        private void TrySignalAllChanged()
        {
            if (ChangedChildrenCount == Children.Count) OnAllChildrenChanged?.Invoke(this);

            ChangedChildrenCount = 0;
        }
    }
}