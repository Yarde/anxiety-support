using UnityEngine;

namespace Yarde.Utils.Extensions
{
    public static class RectTransformExtensions
    {
        public static Vector2 GetWorldPositionOfCanvasElement(this RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Helpers.MainCamera, out var result);
            return result;
        }
    }
}