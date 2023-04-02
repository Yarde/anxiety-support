using VContainer;
using Yarde.Camera;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.Entity
{
    public class Dog : Entity
    {
        private readonly DogView _viewPrefab;
        private readonly CameraManager _cameraManager;

        public Dog(IObjectResolver container) : base(container)
        {
        }

        protected override void SetupInternal()
        {
        }
    }
}