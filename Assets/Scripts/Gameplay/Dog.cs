using VContainer;
using VContainer.Unity;

namespace Yarde.Gameplay
{
    public class Dog : IStartable
    {
        private readonly IObjectResolver _container;
        private readonly DogView _view;
        private readonly GameplayPlane _plane;

        public Dog(IObjectResolver container, DogView view, GameplayPlane plane)
        {
            _container = container;
            _view = view;
            _plane = plane;
        }

        void IStartable.Start()
        {
            _container.Instantiate(_view, _plane.transform);
        }
    }
}