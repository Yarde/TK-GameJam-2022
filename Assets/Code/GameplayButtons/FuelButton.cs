using UnityEngine;

namespace Yarde.GameplayButtons
{
    internal class FuelButton : ActionButton
    {
        [SerializeField] private float _gain = 1;

        protected override void DoAction()
        {
            _gameLoop.State.FireFuel.Value += _gain;
        }
    }
}