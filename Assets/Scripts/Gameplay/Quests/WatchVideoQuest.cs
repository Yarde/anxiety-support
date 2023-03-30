using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Yarde.Quests;

namespace Yarde.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "WatchVideoQuest", menuName = "Quests/WatchVideoQuest", order = 3)]
    public class WatchVideoQuest : Quest
    {
        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            await UniTask.Delay(10000, cancellationToken: cts.Token);
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            await UniTask.Never(cts.Token);
        }
    }
}