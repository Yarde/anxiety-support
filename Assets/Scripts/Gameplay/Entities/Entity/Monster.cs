using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using Yarde.Gameplay.Entities.SpawnPoints;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.Entity
{
    public class Monster : Entity
    {
        private MonsterView _monsterView;

        public Monster(IObjectResolver container, SpawnPoint spawnPoint) : base(container, spawnPoint)
        {
        }

        protected override void SetupInternal()
        {
            _monsterView = View as MonsterView;

            var owner = _container.Resolve<EntityManager>().GetEntityByType(typeof(Owner));
            Assert.IsNotNull(owner, "Owner is null");
            Assert.IsNotNull(_monsterView, "View is null");

            _monsterView.SetTarget(owner.View);
        }

        public override async void TriggerDeath()
        {
            Debug.Log($"Monster {_monsterView.name} died");
            await _monsterView.OnDie();
            if (_monsterView)
            {
                Object.Destroy(_monsterView.gameObject);
            }
        }
    }
}