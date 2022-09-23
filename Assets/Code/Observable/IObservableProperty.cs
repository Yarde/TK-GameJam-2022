using System;
using JetBrains.Annotations;

namespace Yarde.Observable
{
    [PublicAPI]
    public interface IObservableProperty<T>
    {
        Action<IObservableProperty<T>> OnValueChanged { get; set; }
        T Value { get; set; }
    }
}