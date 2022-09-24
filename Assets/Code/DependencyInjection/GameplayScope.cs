using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.EventDispatcher;
using Yarde.Flows;
using Yarde.Gameplay;
using Yarde.WindowSystem;
using Yarde.WindowSystem.BlendProvider;
using Yarde.WindowSystem.CanvasProvider;
using Yarde.WindowSystem.WindowProvider;

namespace Yarde.DependencyInjection
{
    public class GameplayScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterSystems(builder);
        }

        private static void RegisterSystems(IContainerBuilder builder)
        {
            builder.Register<GameState>(Lifetime.Scoped);
        }
    }
}