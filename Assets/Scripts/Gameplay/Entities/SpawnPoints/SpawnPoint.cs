using System.Collections.Generic;
using UnityEngine;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.SpawnPoints
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnPointConfig _config;
        [SerializeField] private float _delay;

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
        public float Delay => _delay;

        private void Awake()
        {
            Transform = transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColors[(int)_config.Type];
            var mesh = _config.ViewPrefab.GetComponentInChildren<SkinnedMeshRenderer>()?.sharedMesh;
            if (mesh == null)
            {
                Gizmos.DrawCube(transform.position + Vector3.up, 
                    new Vector3(1, 2, 1));
            }
            else
            {
                Gizmos.DrawMesh(mesh, 
                    transform.position + Vector3.up, 
                    transform.rotation * Quaternion.Euler(-90, 0 ,0), 
                    Vector3.one * 50);
            }
        }
    }
}