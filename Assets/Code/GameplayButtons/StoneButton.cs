using UnityEngine;

namespace Yarde.GameplayButtons
{
    internal class StoneButton : ActionButton
    {
        [SerializeField] private int _gain = 1;

        protected override void DoAction()
        {
            _gameLoop.State.Stone.Value += _gain;
        }
    }
}