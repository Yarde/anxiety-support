using System;
using System.Collections.Generic;
using UnityEngine;

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

        public void PlayClip(AudioType type, AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            var source = GetSource(type);
            source.clip = clip;
            source.Play();
        }

        private AudioSource GetSource(AudioType type)
        {
            switch (type)
            {
                case AudioType.Music:
                    return _musicSource;
                case AudioType.Sfx:
                    _sfxIndex = (_sfxIndex + 1) % _sfxSources.Count;
                    return _sfxSources[_sfxIndex];
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum AudioType
    {
        Music,
        Sfx
    }
}