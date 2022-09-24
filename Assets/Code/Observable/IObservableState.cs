using System;
using JetBrains.Annotations;

namespace Yarde.Observable
{
    [PublicAPI]
    public interface IObservableState
    {
        /// <summary>
        /// Invoked once the State is consistent and <b>any</b> of it's children have changed
        /// </summary>
        Action<ObservableState> OnStateChanged { get; set; }

        /// <summary>
        /// Invoked once the State is consistent and any <b>all</b> it's children have changed
        /// </summary>
        Action<ObservableState> OnAllChildrenChanged { get; set; }
    }
}