namespace Yarde.GameplayButtons
{
    internal class WallsButton : ActionButton
    {
        protected override void DoAction()
        {
            _gameLoop.State.WallsLevel.Value++;
        }
    }
}