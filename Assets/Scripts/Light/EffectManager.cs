using UnityEngine;
using UnityEngine.Rendering;

namespace Yarde.Light
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Light _light;
        [SerializeField] private Volume _volume;

        public void SetIntensity(float intensity)
        {
            _volume.weight = 1 - intensity;
            _light.color = new Color(intensity, intensity, intensity);
        }
    }
}