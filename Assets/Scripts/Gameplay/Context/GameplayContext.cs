using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.Camera;
using Yarde.Gameplay.Entities;
using Yarde.Gameplay.Scenes;
using Yarde.Input;
using Yarde.Light;
using Yarde.Quests;
using Yarde.UI;

namespace Yarde.Gameplay.Context
{
    public class GameplayContext : SceneContext
    {
        [SerializeField] private Joystick _joystick;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayScene>(Lifetime.Scoped);
            builder.Register<EntityManager>(Lifetime.Scoped);
            builder.Register<InputSystem>(Lifetime.Scoped);
            builder.Register<QuestSystem>(Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<CanvasManager>();
            builder.RegisterComponentInHierarchy<CameraManager>();
            builder.RegisterComponentInHierarchy<GameplayPlane>();
            builder.RegisterComponentInHierarchy<EffectManager>();

            builder.Register(InstantiateJoystick, Lifetime.Scoped);
        }

        private Joystick InstantiateJoystick(IObjectResolver container)
        {
            return container.Instantiate(_joystick, container.Resolve<CanvasManager>().transform);
        }
    }
}