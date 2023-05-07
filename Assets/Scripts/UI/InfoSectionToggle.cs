using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoSectionToggle : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _content;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private float _animationTime = 0.2f;

    private bool _isToggled = true;
    private Vector2 _closedSize;
    private Vector2 _openSize;

    private void Awake()
    {
        _button.onClick.AddListener(() => ToggleSection().Forget());
        var rect = _content.rect;
        _closedSize = new Vector2(rect.width, 0);
        _openSize = rect.size;
        ToggleSection().Forget();
    }

    private async UniTaskVoid ToggleSection()
    {
        if (_isToggled)
        {
            _content.DOSizeDelta(_closedSize, _animationTime);
            _contentText.DOFade(0f, _animationTime);
            _isToggled = false;
        }
        else
        {
            _content.DOSizeDelta(_openSize, _animationTime);
            _contentText.DOFade(1f, _animationTime);
            _isToggled = true;
        }
    }
}