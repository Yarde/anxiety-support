using VContainer.Unity;
using Yarde.Input;

namespace Yarde.Gameplay
{
    public class GameplayBoot : IStartable
    {
        private readonly InputSystem _inputSystem;

        public GameplayBoot(InputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }

        void IStartable.Start()
        {
        }
    }
}