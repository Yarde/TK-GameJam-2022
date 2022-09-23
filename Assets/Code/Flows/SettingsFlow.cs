using Cysharp.Threading.Tasks;
using Yarde.EventDispatcher;
using Yarde.UI;
using Yarde.WindowSystem;
using Yarde.WindowSystem.WindowProvider;

namespace Yarde.Flows
{
    public abstract class SettingsFlowBase : BaseFlow
    {
        protected SettingsFlowBase(IDispatcher dispatcher) : base(dispatcher)
        {
        }
    }

    public class SettingsFlow : SettingsFlowBase
    {
        private readonly IWindowManager _windowManager;

        public SettingsFlow(IDispatcher dispatcher, IWindowManager windowManager) : base(dispatcher) =>
            _windowManager = windowManager;

        protected override async UniTask OnStart(IListener listener)
        {
            listener.AddHandler<SettingsOpenEvent>(OnSettingOpen);
            listener.AddHandler<BackButtonEvent>(_ => Cancel());

            await UniTask.CompletedTask;
        }

        protected override async UniTask OnCancel(IListener listener)
        {
            await _windowManager.Remove(WindowType.Settings);
        }

        private async UniTask OnSettingOpen(SettingsOpenEvent ev)
        {
            await _windowManager.Add<SettingsWindow>(WindowType.Settings, async window => await window.Setup());
        }
    }
}