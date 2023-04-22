using System.Threading;
using Cysharp.Threading.Tasks;
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

            var owner = _container.Resolve<EntityManager>().GetEntityByType<Owner>();
            Assert.IsNotNull(owner, "Owner is null");
            Assert.IsNotNull(_monsterView, "View is null");

            _monsterView.SetTarget(owner.View);
            TryAttack(owner, _monsterView.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid TryAttack(Owner owner, CancellationToken ctx)
        {
            while (!ctx.IsCancellationRequested)
            {
                if (_monsterView.IsAttackingDistance)
                {
                    owner.TakeDamage(1);
                    await _monsterView.Attack();
                }
                else
                {
                    await UniTask.Delay(1000, cancellationToken: ctx);
                }
            }
        }

        public override bool TakeDamage(int damage)
        {
            Health -= damage;

            if (Health > 0)
            {
                return false;
            }
            
            Debug.Log($"Monster {_monsterView.name} died");
            _monsterView.OnDie().Forget();

            return true;
        }
    }
}