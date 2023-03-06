using Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yarde.Input;
using Yarde.UI;

namespace Yarde.Gameplay
{
    public class GameplayContext : LifetimeScope
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private DogView _dogView;
        [SerializeField] private GameplayPlane _plane;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayBoot>(Lifetime.Scoped);
            
            builder.Register<InputSystem>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<CanvasManager>();
            
            builder.Register(InstantiateJoystick, Lifetime.Scoped);
            
            builder.RegisterComponent(_plane);
            
            builder.RegisterComponent(_dogView);
            builder.RegisterEntryPoint<Dog>(Lifetime.Scoped);
        }

        private Joystick InstantiateJoystick(IObjectResolver container)
        {
            return container.Instantiate(_joystick, container.Resolve<CanvasManager>().transform);
        }
    }
}