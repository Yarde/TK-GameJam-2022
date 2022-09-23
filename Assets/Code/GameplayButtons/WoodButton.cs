using UnityEngine;

namespace Yarde.GameplayButtons
{
    internal class WoodButton : ActionButton
    {
        [SerializeField] private int _gain = 1;

        protected override void DoAction()
        {
            _gameLoop.State.Wood.Value += _gain;
        }
    }
}