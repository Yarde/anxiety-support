using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Yarde.Gameplay.Entities.SpawnPoints;

namespace Yarde.Gameplay.Entities.Entity
{
    public class BossMonster : Monster
    {
        public BossMonster(IObjectResolver container, SpawnPoint spawnPoint) : base(container, spawnPoint)
        {
        }

        protected override async UniTask Attack(Owner owner)
        {
            await base.Attack(owner);
            await Teleport();
        }

        private async UniTask Teleport()
        {
            await UniTask.Delay(1000);
            await _monsterView.Fade(0, 0.5f);
            _monsterView.transform.position = GetRandomPosition();
            await _monsterView.Fade(1, 0.5f);
        }

        private Vector3 GetRandomPosition()
        {
            var randomPosition = Random.insideUnitCircle * 20;
            return new Vector3(randomPosition.x, 0, randomPosition.y);
        }
    }
}