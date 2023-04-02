using UnityEngine;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.SpawnPoint
{
    [CreateAssetMenu]
    public class SpawnPointConfig : ScriptableObject
    {
        [field: SerializeField] public EntityView ViewPrefab { get; private set; }
        [field: SerializeField] public EntityType Type { get; private set; }
    }

    public enum EntityType
    {
        Dog,
        Human,
        Owner
    }
}