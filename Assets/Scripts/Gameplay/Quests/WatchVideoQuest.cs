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
        [SerializeField] private string _videoUrl;
        [Inject] [UsedImplicitly] private VideoPlayer _videoPlayer;

        protected override void RunInternal()
        {
        }

        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            _videoPlayer.source = Application.platform == RuntimePlatform.WebGLPlayer
                ? VideoSource.Url
                : VideoSource.VideoClip;
            _videoPlayer.gameObject.SetActive(true);
            _videoPlayer.clip = _videoClip;
            _videoPlayer.url = _videoUrl;
            var length = (int)(_videoClip.length * 1000);
            Debug.Log($"Playing video {_videoClip.name}, length: {length} ms");
            _videoPlayer.Play();
            await UniTask.Delay(length, cancellationToken: cts.Token);
            _videoPlayer.gameObject.SetActive(false);
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            await UniTask.Never(cts.Token);
        }
    }
}