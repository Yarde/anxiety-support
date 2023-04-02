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
    [CreateAssetMenu(fileName = "FindOwnerQuest", menuName = "Quests/FindOwnerQuest", order = 2)]
    public class FindOwnerQuest : Quest
    {
        [SerializeField] private float _timeToFinish;

        [Inject] [UsedImplicitly] private EntityManager _entityManager;

        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            var dog = _entityManager.GetEntityByType(typeof(Dog));
            var human = _entityManager.GetEntityByType(typeof(Owner));
            await UniTask.WaitUntil(() => (dog.View.transform.position - human.View.transform.position).magnitude < 1,
                cancellationToken: cts.Token);
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            await UniTask.WaitWhile(() => (_timeToFinish -= Time.deltaTime) > 0, cancellationToken: cts.Token);
        }
    }
}