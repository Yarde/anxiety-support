using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening.Plugins.Core.PathCore;
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
        [SerializeField] private string _videoName;
        [SerializeField] private float _videoLength;
        [Inject] [UsedImplicitly] private VideoPlayer _videoPlayer;

        protected override void RunInternal()
        {
        }

        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            _videoPlayer.gameObject.SetActive(true);
            var videoUrl = System.IO.Path.Combine(Application.streamingAssetsPath, _videoName);
            _videoPlayer.url = videoUrl;
            _videoPlayer.Prepare();
            var length = (int)(_videoLength * 1000);
            Debug.Log($"Playing video {_videoName}, length: {length} ms");
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