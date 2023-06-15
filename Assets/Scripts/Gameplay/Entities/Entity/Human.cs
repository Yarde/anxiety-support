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
        private const float MovementRange = 5f;
        private const float ChanceOfMoving = 0.6f;

        private HumanView _humanView;
        private Vector3 _startPosition;

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
            _startPosition = _humanView.transform.position;
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
                var x = Random.Range(_startPosition.x - MovementRange, _startPosition.x + MovementRange);
                var z = Random.Range(_startPosition.z - MovementRange, _startPosition.z + MovementRange);
                _humanView.SetTarget(new Vector3(x, 0, z));
                await UniTask.WaitUntil(_humanView.IsReachedTarget, cancellationToken: cancellationToken);
            }
            else
            {
                await UniTask.Delay((int)(Random.Range(1f, 5f) * 1000), cancellationToken: cancellationToken);
            }
        }

        public override bool TakeDamage(int damage)
        {
            return false;
        }
    }
}