using VContainer;
using Yarde.Gameplay.Entities.SpawnPoints;

namespace Yarde.Gameplay.Entities.Entity
{
    public class Owner : Entity
    {
        public int Health { get; private set; }
        
        public Owner(IObjectResolver container, SpawnPoint spawnPoint) : base(container, spawnPoint)
        {
            Health = 120;
        }

        protected override void SetupInternal()
        {
        }

        public override bool TakeDamage(int damage)
        {
            return false;
        }
    }
}