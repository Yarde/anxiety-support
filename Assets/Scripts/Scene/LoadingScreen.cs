using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Yarde.Scene
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private float _fadeTime = 0.2f;

        public async UniTask StartLoading(bool showAnimation)
        {
            gameObject.SetActive(true);
            
            var animations = new List<UniTask> { _background.DOFade(1f, _fadeTime).ToUniTask() };
            if (showAnimation)
            {
                animations.Add(_renderer.DOFade(1f, _fadeTime).ToUniTask());
            }

            await animations;
        }

        public async UniTask StopLoading(bool showAnimation)
        {
            var animations = new List<UniTask> { _background.DOFade(0f, _fadeTime).ToUniTask() };

            if (showAnimation)
            {
                animations.Add(_renderer.DOFade(0f, _fadeTime).ToUniTask());
            }

            gameObject.SetActive(false);
            await animations;
        }
    }
}