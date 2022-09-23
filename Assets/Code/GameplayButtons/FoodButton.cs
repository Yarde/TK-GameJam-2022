using UnityEngine;

namespace Yarde.GameplayButtons
{
    internal class FoodButton : ActionButton
    {
        [SerializeField] private int _gain = 1;

        protected override void DoAction()
        {
            _gameLoop.State.Food.Value += _gain;
        }
    }
}