using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using Yarde.Quests;

namespace Yarde.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "WatchVideoQuest", menuName = "Quests/WatchVideoQuest", order = 3)]
    public class WatchVideoQuest : Quest
    {
        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            var videoPlayer = FindObjectOfType<VideoPlayer>();
            var length = (int)(videoPlayer.length * 1000);
            Debug.Log($"Video length: {length} ms");
            await UniTask.Delay(length, cancellationToken: cts.Token);
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            await UniTask.Never(cts.Token);
        }
    }
}