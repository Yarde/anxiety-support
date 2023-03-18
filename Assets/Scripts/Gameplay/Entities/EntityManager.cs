using System;
using System.Collections.Generic;
using VContainer;
using Yarde.Camera;
using Object = UnityEngine.Object;

namespace Yarde.Gameplay.Entities
{
    public class EntityManager
    {
        private List<Entity> _entities;

        public EntityManager(IObjectResolver container, CameraManager cameraManager)
        {
            _entities = new List<Entity>();

            var spawnPoints = Object.FindObjectsOfType<SpawnPoint>();
            foreach (var spawnPoint in spawnPoints)
            {
                var entity = CreateEntity(spawnPoint.Type, container, cameraManager);
                entity.Setup(spawnPoint);
                _entities.Add(entity);
            }
        }

        private static Entity CreateEntity(EntityType type, IObjectResolver container, CameraManager cameraManager)
        {
            return type switch
            {
                EntityType.Dog => new Dog(container, cameraManager),
                EntityType.Human => new Human(container),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}