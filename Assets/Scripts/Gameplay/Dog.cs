using VContainer;
using VContainer.Unity;
using Yarde.Camera;

namespace Yarde.Gameplay
{
    public class Dog : IStartable
    {
        private readonly IObjectResolver _container;
        private readonly DogView _viewPrefab;
        private readonly GameplayPlane _plane;
        private readonly CameraManager _cameraManager;

        public DogView View { get; private set; }

        public Dog(IObjectResolver container, DogView viewPrefab, GameplayPlane plane, CameraManager cameraManager)
        {
            _container = container;
            _viewPrefab = viewPrefab;
            _plane = plane;
            _cameraManager = cameraManager;
        }

        void IStartable.Start()
        {
            View = _container.Instantiate(_viewPrefab, _plane.transform);
            _cameraManager.SelectTarget(View.transform);
        }
    }
}