using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using Yarde.Gameplay.Entities;
using Yarde.Gameplay.Entities.Entity;
using Yarde.Light;
using Yarde.Quests;

namespace Yarde.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "FightMonstersQuest", menuName = "Quests/FightMonstersQuest", order = 4)]
    public class FightMonstersQuest : Quest
    {
        [Inject] [UsedImplicitly] private EntityManager _entityManager;
        [Inject] [UsedImplicitly] private EffectManager _effectManager;

        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            AdjustLight(cts).Forget();
            await UniTask.Delay(10000);
            await UniTask.WaitUntil(() => _entityManager.GetEntityByType<Monster>() == null);
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            await UniTask.WaitUntil(() => _entityManager.GetEntityByType<Owner>() != null,
                cancellationToken: cts.Token);
            await UniTask.WaitUntil(() => _entityManager.GetEntityByType<Owner>().Health <= 0,
                cancellationToken: cts.Token);
        }

        private async UniTaskVoid AdjustLight(CancellationTokenSource cts)
        {
            var owner = _entityManager.GetEntityByType<Owner>();
            while (!cts.IsCancellationRequested)
            {
                var state = owner.Health / 50f;
                _effectManager.SetIntensity(state);
                await UniTask.Delay(100);
            }
        }
    }
}