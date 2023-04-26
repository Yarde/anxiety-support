using UnityEngine;
using Yarde.Gameplay.Entities.View;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.Entities.SpawnPoints
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private SpawnPointConfig _config;

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
    }
}