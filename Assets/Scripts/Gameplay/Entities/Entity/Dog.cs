using VContainer;
using Yarde.Gameplay.Entities.SpawnPoints;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.Entity
{
    public class Dog : Entity
    {
        private DogView _dogView;

        public Dog(IObjectResolver container, SpawnPoint spawnPoint) : base(container, spawnPoint)
        {
        }

        protected override void SetupInternal()
        {
            _dogView = View as DogView;
        }

        public override void TriggerDeath()
        {
        }
    }
}