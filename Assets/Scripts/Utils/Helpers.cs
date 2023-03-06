using UnityEngine;

namespace Yarde.Utils
{
    public static class Helpers
    {
        private static Camera _mainCamera;
        public static Camera MainCamera => _mainCamera ??= Camera.main;
    
    }
}