using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Yarde.Scene
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private float _fadeTime = 0.2f;

        public async UniTask StartLoading()
        {
            gameObject.SetActive(true);
            await _renderer.DOFade(1f, _fadeTime);
        }

        public async UniTask StopLoading()
        {
            await _renderer.DOFade(0f, _fadeTime);
            gameObject.SetActive(false);
        }
    }
}