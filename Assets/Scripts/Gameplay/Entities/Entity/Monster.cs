using UnityEngine.Assertions;
using VContainer;
using Yarde.Gameplay.Entities.SpawnPoints;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.Entity
{
    public class Monster : Entity
    {
        public Monster(IObjectResolver container, SpawnPoint spawnPoint) : base(container, spawnPoint)
        {
        }

        protected override void SetupInternal()
        {
            var owner = _container.Resolve<EntityManager>().GetEntityByType(typeof(Owner));
            Assert.IsNotNull(owner, "Owner is null");
            
            (View as MonsterView)?.SetTarget(owner.View);
        }
    }
}