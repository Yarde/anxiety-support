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
            if (isMoving > 0.5f)
            {
                var randomPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
                _humanView.SetTarget(randomPosition);
                await UniTask.WaitUntil(_humanView.IsReachedTarget, cancellationToken: cancellationToken);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            var randomDelay = Random.Range(1f, 5f);
            await UniTask.Delay((int)(randomDelay * 1000), cancellationToken: cancellationToken);
        }

        public override void TriggerDeath()
        {
        }
    }
}