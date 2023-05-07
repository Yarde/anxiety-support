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
        protected MonsterView _monsterView;

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
                    await Attack(owner);
                }

                await UniTask.Delay((int)(Random.Range(1f, 3f) * 1000), cancellationToken: ctx);
            }
        }

        protected virtual async UniTask Attack(Owner owner)
        {
            owner.TakeDamage(1);
            await _monsterView.Attack();
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