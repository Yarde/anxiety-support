using System.Collections.Generic;
using UnityEngine;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.SpawnPoints
{
    [CreateAssetMenu]
    public class SpawnPointConfig : ScriptableObject
    {
        [field: SerializeField] public List<EntityView> ViewPrefab { get; private set; }
        [field: SerializeField] public EntityType Type { get; private set; }
        
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public int Repeats { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
    }

    public enum EntityType
    {
        Dog,
        Human,
        Owner,
        Monster
    }
}