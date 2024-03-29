﻿using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.EventDispatcher;
using Yarde.Flows;
using Yarde.WindowSystem;
using Yarde.WindowSystem.BlendProvider;
using Yarde.WindowSystem.CanvasProvider;
using Yarde.WindowSystem.WindowProvider;

namespace Yarde.DependencyInjection
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private BlendViewBase blendViewBase;
        [SerializeField] private CanvasManagerBase canvasManagerBase;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterSystems(builder);
            RegisterFlows(builder);
            RegisterComponents(builder);
        }

        private static void RegisterSystems(IContainerBuilder builder)
        {
            builder.Register<IWindowManager, DictionaryWindowManager>(Lifetime.Singleton);
            builder.Register<IWindowProvider, ScriptableObjectWindowProvider>(Lifetime.Singleton);
            builder.Register<IDispatcher, ListDispatcher>(Lifetime.Singleton);
        }

        private static void RegisterFlows(IContainerBuilder builder)
        {
            builder.Register<RootFlowBase, RootFlow>(Lifetime.Singleton);
            builder.Register<MenuFlowBase, MenuFlow>(Lifetime.Singleton);
            builder.Register<SettingsFlowBase, SettingsFlow>(Lifetime.Singleton);
        }

        private void RegisterComponents(IContainerBuilder builder)
        {
            builder.RegisterComponent(blendViewBase);
            builder.RegisterComponent(canvasManagerBase);
        }
    }
}