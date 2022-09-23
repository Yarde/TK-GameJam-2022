namespace Yarde.GameplayButtons
{
    internal class TentButton : ActionButton
    {
        protected override void DoAction()
        {
            _gameLoop.State.TentLevel.Value++;
        }
    }
}