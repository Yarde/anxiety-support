using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Video;
using VContainer;
using Yarde.Quests;

namespace Yarde.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "WatchVideoQuest", menuName = "Quests/WatchVideoQuest", order = 3)]
    public class WatchVideoQuest : Quest
    {
        [SerializeField] private VideoClip _videoClip;
        [Inject] [UsedImplicitly] private VideoPlayer _videoPlayer;

        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            _videoPlayer.clip = _videoClip;
            var length = (int)(_videoClip.length * 1000);
            Debug.Log($"Playing video {_videoClip.name}, length: {length} ms");
            _videoPlayer.Play();
            await UniTask.Delay(length, cancellationToken: cts.Token);
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            await UniTask.Never(cts.Token);
        }
    }
}