using Cinemachine;
using UnityEngine;

namespace Yarde.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        
        public void SelectTarget(Transform toFollow)
        {
            _camera.Follow = toFollow;
        }
    }
}