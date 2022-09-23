namespace Yarde.GameplayButtons
{
    internal class FireplaceButton : ActionButton
    {
        protected override void DoAction()
        {
            _gameLoop.State.FireplaceLevel.Value++;
        }
    }
}