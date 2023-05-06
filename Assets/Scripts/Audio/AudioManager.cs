using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Yarde.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private List<AudioSource> _sfxSources;
        
        private int _sfxIndex;

        private void Awake()
        {
            _musicSource.loop = true;
        }

        public void PlayMusic(string trackName)
        {
            Debug.Log($"Playing music {trackName}");
            var clip = Resources.Load<AudioClip>($"Audio/Music/{trackName}");
            Assert.IsNotNull(clip, $"Clip {trackName} not found");
            _musicSource.clip = clip;
            _musicSource.Play();
        }
        
        public void PlaySfx(string trackName)
        {
            Debug.Log($"Playing sfx {trackName} on source {_sfxIndex + 1}");
            var source = _sfxSources[_sfxIndex];
            var clip = Resources.Load<AudioClip>($"Audio/Sfx/{trackName}");
            Assert.IsNotNull(clip, $"Clip {trackName} not found");
            source.clip = clip;
            source.Play();
            
            _sfxIndex = (_sfxIndex + 1) % _sfxSources.Count;
        }
    }
}