using UnityEngine;
using UnityEngine.Rendering;
using Yarde.Utils.Extensions;

namespace Yarde.Light
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Light _light;
        [SerializeField] private Volume _volume;
        [SerializeField] private MeshRenderer _crack;

        public void SetIntensity(float intensity)
        {
            _volume.weight = 1 - intensity / 2;
            _light.color = new Color(intensity, intensity, intensity);
            _crack.material.SetAlpha(1f - intensity);
        }
    }
}