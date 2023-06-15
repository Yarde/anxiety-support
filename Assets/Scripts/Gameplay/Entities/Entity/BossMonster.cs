using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Yarde.Gameplay.Entities.SpawnPoints;

namespace Yarde.Gameplay.Entities.Entity
{
    public class BossMonster : Monster
    {
        private CharacterController _characterController;
        
        public BossMonster(IObjectResolver container, SpawnPoint spawnPoint) : base(container, spawnPoint)
        {
        }

        private CancellationToken Token { get; set; }

        protected override void SetupInternal()
        {
            base.SetupInternal();
            _characterController = _monsterView.GetComponent<CharacterController>();
            Token = _monsterView.GetCancellationTokenOnDestroy();
        }

        protected override async UniTask Attack(Owner owner)
        {
            await base.Attack(owner);
            await Teleport();
        }

        public override bool TakeDamage(int damage)
        {
            Health -= damage;
            Teleport().Forget();

            if (Health > 0)
            {
                return false;
            }

            Debug.Log("Boss died!");
            _monsterView.OnDie().Forget();

            return true;
        }

        private async UniTask Teleport()
        {
            _characterController.enabled = false;
            await UniTask.Delay(1000, cancellationToken: Token);
            await _monsterView.Fade(0, 0.5f);
            if (Token.IsCancellationRequested)
            {
                return;
            }
            _monsterView.transform.position = GetRandomPosition();
            await _monsterView.Fade(1, 0.5f);
            _characterController.enabled = true;
        }

        private Vector3 GetRandomPosition()
        {
            var randomPosition = Random.insideUnitCircle * 20;
            return new Vector3(randomPosition.x, 0, randomPosition.y);
        }
    }
}