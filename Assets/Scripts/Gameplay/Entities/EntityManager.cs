using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using VContainer;
using Yarde.Camera;
using Yarde.Gameplay.Entities.Entity;
using Yarde.Gameplay.Entities.SpawnPoint;
using Object = UnityEngine.Object;

namespace Yarde.Gameplay.Entities
{
    [UsedImplicitly]
    public class EntityManager : IDisposable
    {
        private readonly List<Entity.Entity> _entities;
        private readonly IObjectResolver _container;

        public EntityManager(IObjectResolver container, CameraManager cameraManager)
        {
            _container = container;
            _entities = new List<Entity.Entity>();
        }

        public void Dispose()
        {
            _entities.Clear();
        }

        public Entity.Entity GetEntityByType(Type type)
        {
            return _entities.FirstOrDefault(e => e.GetType() == type);
        }

        public void Setup()
        {
            SpawnAllEntities();
        }

        private void SpawnAllEntities()
        {
            var spawnPoints = Object.FindObjectsOfType<SpawnPoint.SpawnPoint>();
            foreach (var spawnPoint in spawnPoints)
            {
                var entity = CreateEntity(spawnPoint.Type, _container);
                entity.Setup(spawnPoint);
                _entities.Add(entity);
            }
        }

        private static Entity.Entity CreateEntity(EntityType type, IObjectResolver container)
        {
            return type switch
            {
                EntityType.Dog => new Dog(container),
                EntityType.Human => new Human(container),
                EntityType.Owner => new Owner(container),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}