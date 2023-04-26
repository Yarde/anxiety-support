using UnityEngine;
using UnityEngine.EventSystems;

namespace Yarde.Input
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float Horizontal => _input.x;
        public float Vertical => _input.y;

        [SerializeField] private float _deadZone;

        [SerializeField] protected RectTransform _background;
        [SerializeField] private RectTransform _handle;
        
        [Header("Display options")]
        [SerializeField] private bool _autoHide;

        private RectTransform _baseRect;
        private Canvas _canvas;

        private Vector2 _input = Vector2.zero;
        private Vector2 _radius;

        private void Start()
        {
            _baseRect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();

            var center = new Vector2(0.5f, 0.5f);
            _background.pivot = center;
            _handle.anchorMin = center;
            _handle.anchorMax = center;
            _handle.pivot = center;
            _radius = _background.sizeDelta / 2;

            Reset();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            if (_autoHide)
            {
                _background.gameObject.SetActive(true);
            }
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _input = (eventData.position - (Vector2)_background.position) / (_radius * _canvas.scaleFactor);
            if (_input.magnitude < _deadZone)
            {
                _input = Vector2.zero;
            }

            if (_input.magnitude > 1)
            {
                _input = _input.normalized;
            }

            _handle.anchoredPosition = _input * _radius;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Reset();
        }

        private void Reset()
        {
            _input = Vector2.zero;
            _handle.anchoredPosition = Vector2.zero;
            if (_autoHide)
            {
                _background.gameObject.SetActive(false);
            }
        }

        private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, null,
                    out var localPoint))
            {
                var sizeDelta = _baseRect.sizeDelta;
                var pivotOffset = _baseRect.pivot * sizeDelta;
                return localPoint - _background.anchorMax * sizeDelta + pivotOffset;
            }

            return Vector2.zero;
        }
    }
}