using System.Collections.Generic;
using UnityEngine;

namespace Yarde.Gameplay.Entities
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnPointConfig _config;

        private readonly List<Color> _gizmoColors = new()
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.cyan,
            Color.magenta,
            Color.gray,
            Color.white,
            Color.black
        };

        public EntityType Type => _config.Type;
        public Transform Transform { get; private set; }
        public EntityView Prefab => _config.ViewPrefab;

        private void Awake()
        {
            Transform = transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColors[(int)_config.Type];
            var mesh = _config.ViewPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
            Gizmos.DrawMesh(mesh, 
                transform.position + Vector3.up, 
                transform.rotation * Quaternion.Euler(-90, 0 ,0), 
                Vector3.one * 50);
        }
    }
}