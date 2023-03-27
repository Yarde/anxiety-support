using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.Camera;
using Yarde.Gameplay.Entities;
using Yarde.Input;
using Yarde.Quests;
using Yarde.UI;

namespace Yarde.Gameplay
{
    public class GameplayContext : LifetimeScope
    {
        [SerializeField] private Joystick _joystick;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayBoot>(Lifetime.Scoped);
            
            builder.Register<EntityManager>(Lifetime.Scoped);
            builder.Register<InputSystem>(Lifetime.Scoped);
            builder.Register<QuestSystem>(Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<CanvasManager>();
            builder.RegisterComponentInHierarchy<CameraManager>();
            builder.RegisterComponentInHierarchy<GameplayPlane>();

            builder.Register(InstantiateJoystick, Lifetime.Scoped);
        }

        private Joystick InstantiateJoystick(IObjectResolver container)
        {
            return container.Instantiate(_joystick, container.Resolve<CanvasManager>().transform);
        }
    }
}