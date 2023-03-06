using VContainer.Unity;
using Yarde.Input;

namespace Yarde.Gameplay
{
    public class GameplayBoot : IStartable
    {
        private readonly InputSystem _container;

        public GameplayBoot(InputSystem container)
        {
            _container = container;
        }

        void IStartable.Start()
        {
            _container.GetMovementNormalized();
        }
    }
}