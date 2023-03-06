using System;
using Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.Gameplay;
using Yarde.Input;
using Yarde.UI;

namespace Yarde.DependencyInjection
{
    public class GameplaySceneContext : LifetimeScope
    {
        [SerializeField] private Joystick _joystick;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<CanvasManager>();
            builder.RegisterEntryPoint<GameplayBoot>();
            builder.Register(InstantiateJoystick, Lifetime.Scoped);
            builder.Register<InputSystem>(Lifetime.Scoped);
        }

        private Joystick InstantiateJoystick(IObjectResolver container)
        {
            return container.Instantiate(_joystick, container.Resolve<CanvasManager>().transform);
        }
    }
}