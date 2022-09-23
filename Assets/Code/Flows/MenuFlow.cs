using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Yarde.EventDispatcher;
using Yarde.UI;
using Yarde.WindowSystem;
using Yarde.WindowSystem.WindowProvider;

namespace Yarde.Flows
{
    public abstract class MenuFlowBase : BaseFlow
    {
        protected MenuFlowBase(IDispatcher dispatcher) : base(dispatcher)
        {
        }
    }

    public class MenuFlow : MenuFlowBase
    {
        private readonly IWindowManager _windowManager;
        private readonly SettingsFlowBase _settingsFlow;

        public MenuFlow(IDispatcher dispatcher, IWindowManager windowManager, SettingsFlowBase settingsFlow) :
            base(dispatcher)
        {
            _windowManager = windowManager;
            _settingsFlow = settingsFlow;
        }

        protected override async UniTask OnStart(IListener listener)
        {
            listener.AddHandler<MenuOpenEvent>(OnMenuOpen);
            listener.AddHandler<GameStartedEvent>(OnGameStarted);
            listener.AddHandler<SettingsOpenEvent>(OnSettingOpen);

            await UniTask.CompletedTask;
        }

        protected override async UniTask OnCancel(IListener listener)
        {
            await OnMenuClose();
        }

        private async UniTask OnMenuOpen(MenuOpenEvent ev)
        {
            await _windowManager.Add<MenuWindow>(WindowType.Menu, async window => await window.Setup());
        }

        private async UniTask OnMenuClose()
        {
            await _windowManager.Remove(WindowType.Menu);
        }

        private async UniTask OnGameStarted(GameStartedEvent ev)
        {
            await SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            await Cancel();
        }

        private async UniTask OnSettingOpen(SettingsOpenEvent ev)
        {
            await _settingsFlow.Start(ev);
        }
    }

    public sealed class MenuOpenEvent : IEvent
    {
    }

    public sealed class GameStartedEvent : IEvent
    {
    }

    public sealed class SettingsOpenEvent : IEvent
    {
    }
}