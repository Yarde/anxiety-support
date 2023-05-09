using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Yarde.Quests;

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

        public async UniTask ShowInfo(UiTooltip info)
        {
            if (!info.HasText)
            {
                return;
            }

            _infoText.text = info.Text;
            _infoText.color = info.Color;
            await _canvasGroup.DOFade(1f, 0.3f);
            await UniTask.Delay(2000);
            await _canvasGroup.DOFade(0f, 0.3f);
        }
    }
}