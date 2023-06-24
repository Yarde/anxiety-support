using UnityEngine;
using UnityEngine.Video;

namespace Yarde.Utils
{
    public class PlayVideoFromURL : MonoBehaviour
    {
        [SerializeField] private string _videoName;
        [SerializeField] private VideoPlayer _videoPlayer;
        
        private void Start()
        {
            var videoUrl = System.IO.Path.Combine(Application.streamingAssetsPath, _videoName);
            _videoPlayer.url = videoUrl;
            _videoPlayer.Prepare();
            _videoPlayer.Play();
        }
    }
}
