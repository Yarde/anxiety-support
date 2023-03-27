using UnityEngine;
using UnityEngine.EventSystems;

namespace Yarde.Input
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float Horizontal => _input.x;
        public float Vertical => _input.y;
        
        [SerializeField] private float deadZone = 0;

        [SerializeField] protected RectTransform background;
        [SerializeField] private RectTransform handle;
        
        private RectTransform _baseRect;
        private Canvas _canvas;
        private Camera _camera;

        private Vector2 _input = Vector2.zero;

        private void Start()
        {
            _baseRect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");

            Vector2 center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
            background.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _camera = null;
            if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
                _camera = _canvas.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(_camera, background.position);
            Vector2 radius = background.sizeDelta / 2;
            _input = (eventData.position - position) / (radius * _canvas.scaleFactor);
            HandleInput(_input.magnitude, _input.normalized, radius, _camera);
            handle.anchoredPosition = _input * radius;
        }

        private void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                    _input = normalised;
            }
            else
                _input = Vector2.zero;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            _input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
        }

        private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            Vector2 localPoint = Vector2.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _camera, out localPoint))
            {
                Vector2 pivotOffset = _baseRect.pivot * _baseRect.sizeDelta;
                return localPoint - (background.anchorMax * _baseRect.sizeDelta) + pivotOffset;
            }

            return Vector2.zero;
        }
    }
}