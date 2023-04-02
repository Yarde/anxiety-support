using VContainer;
using Yarde.Gameplay.Entities.SpawnPoints;

namespace Yarde.Gameplay.Entities.Entity
{
    public class Owner : Entity
    {
        public Owner(IObjectResolver container, SpawnPoint spawnPoint) : base(container, spawnPoint)
        {
        }

        protected override void SetupInternal()
        {
        }

        public override void TriggerDeath()
        {
        }
    }
}