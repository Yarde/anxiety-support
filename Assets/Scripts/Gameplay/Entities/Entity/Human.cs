using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Yarde.Gameplay.Entities.SpawnPoints;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.Entity
{
    public class Human : Entity
    {
        private const float PlaneSize = 15f;
        private const float ChanceOfMoving = 0.4f;

        private HumanView _humanView;

        public Human(IObjectResolver container, SpawnPoint spawnPoint) : base(container, spawnPoint)
        {
        }

        protected override void SetupInternal()
        {
            _humanView = View as HumanView;

            StartRandomMovement().Forget();
        }

        private async UniTaskVoid StartRandomMovement()
        {
            var token = _humanView.GetCancellationTokenOnDestroy();
            while (!token.IsCancellationRequested)
            {
                await DoMove(token);
            }
        }

        private async UniTask DoMove(CancellationToken cancellationToken)
        {
            var isMoving = Random.Range(0f, 1f);
            if (isMoving < ChanceOfMoving)
            {
                var randomPosition = new Vector3(Random.Range(-PlaneSize, PlaneSize), 0,
                    Random.Range(-PlaneSize, PlaneSize));
                _humanView.SetTarget(randomPosition);
                await UniTask.WaitUntil(_humanView.IsReachedTarget, cancellationToken: cancellationToken);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
            
            await UniTask.Delay((int)(Random.Range(1f, 5f) * 1000), cancellationToken: cancellationToken);
        }

        public override bool TakeDamage(int damage)
        {
            return false;
        }
    }
}