using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using Yarde.Gameplay.Entities;
using Yarde.Gameplay.Entities.Entity;
using Yarde.Quests;

namespace Yarde.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "FightMonstersQuest", menuName = "Quests/FightMonstersQuest", order = 4)]
    public class FightMonstersQuest : Quest
    {
        [Inject] [UsedImplicitly] private EntityManager _entityManager;

        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
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
    }
}