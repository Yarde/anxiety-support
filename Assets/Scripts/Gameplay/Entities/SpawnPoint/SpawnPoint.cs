using UnityEngine;

namespace Yarde.Gameplay.Entities
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnPointConfig _config;

        public EntityType Type => _config.Type;
        public Vector3 Position { get; private set; }
        public Transform Parent  { get; private set; } 
        public EntityView Prefab => _config.ViewPrefab;

        private void Awake()
        {
            var t = transform;
            Position = t.position;
            Parent = t.parent;
        }
    }
}