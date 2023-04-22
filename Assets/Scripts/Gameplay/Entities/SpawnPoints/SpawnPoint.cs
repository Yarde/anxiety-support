using System.Collections.Generic;
using UnityEngine;
using Yarde.Gameplay.Entities.View;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.Entities.SpawnPoints
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
        public EntityView Prefab => _config.ViewPrefab.Random();
        public float Delay => Random.Range(_config.Delay, _config.Delay * 2);
        public float Repeats { get; set; }
        public float Cooldown => Random.Range(_config.Cooldown, _config.Cooldown * 2);
        public float Health => _config.Health;

        private void Awake()
        {
            Transform = transform;
            Repeats = _config.Repeats;
        }

        /*private void OnDrawGizmos()
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
        }*/
    }
}