using UnityEngine;

namespace Yarde.Gameplay.Entities
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