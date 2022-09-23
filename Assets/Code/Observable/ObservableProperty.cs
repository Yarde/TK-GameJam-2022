using System;

namespace Yarde.Observable
{
    public class ObservableProperty<T> : ObservableBase, IObservableProperty<T>
    {
        private T _currentValue;

        public ObservableProperty(T initialValue, ObservableBase parent) : base(parent)
        {
            _currentValue = initialValue;
        }

        public Action<IObservableProperty<T>> OnValueChanged { get; set; }

        public T Value
        {
            get => _currentValue;
            set
            {
                if (_currentValue == null || !_currentValue.Equals(value))
                {
                    _currentValue = value;
                    SignalChange();
                }
            }
        }

        protected override void InvokeChange()
        {
            base.InvokeChange();
            OnValueChanged?.Invoke(this);
        }
    }
}