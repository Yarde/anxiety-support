using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Yarde.UI
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _infoText;

        private void Start()
        {
            _canvasGroup.alpha = 0f;
        }

        public async UniTask ShowInfo(string info, Color currentQuestColor)
        {
            _infoText.text = info;
            await _canvasGroup.DOFade(1f, 0.3f);
            await UniTask.Delay(2000);
            await _canvasGroup.DOFade(0f, 0.3f);
        }
    }
}