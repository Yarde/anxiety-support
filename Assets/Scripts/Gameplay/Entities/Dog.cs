using VContainer;
using Yarde.Camera;

namespace Yarde.Gameplay.Entities
{
    public class Dog : Entity
    {
        private readonly DogView _viewPrefab;
        private readonly GameplayPlane _plane;
        private readonly CameraManager _cameraManager;

        public Dog(IObjectResolver container) : base(container)
        {
        }

        protected override void SetupInternal()
        {
        }
    }
}