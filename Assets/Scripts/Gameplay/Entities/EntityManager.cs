using System;
using System.Collections.Generic;
using System.Linq;
using VContainer;
using VContainer.Unity;
using Yarde.Camera;
using Object = UnityEngine.Object;

namespace Yarde.Gameplay.Entities
{
    public class EntityManager : IStartable
    {
        private readonly List<Entity> _entities;
        private readonly CameraManager _cameraManager;
        private readonly IObjectResolver _container;

        public EntityManager(IObjectResolver container, CameraManager cameraManager)
        {
            _container = container;
            _cameraManager = cameraManager;
            _entities = new List<Entity>();
        }

        void IStartable.Start()
        {
            SpawnAllEntities();

            _cameraManager.SelectTarget(_entities.FirstOrDefault(e => e.GetType() == typeof(Dog))?.View.transform);
        }

        private void SpawnAllEntities()
        {
            var spawnPoints = Object.FindObjectsOfType<SpawnPoint>();
            foreach (var spawnPoint in spawnPoints)
            {
                var entity = CreateEntity(spawnPoint.Type, _container);
                entity.Setup(spawnPoint);
                _entities.Add(entity);
            }
        }

        private static Entity CreateEntity(EntityType type, IObjectResolver container)
        {
            return type switch
            {
                EntityType.Dog => new Dog(container),
                EntityType.Human => new Human(container),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}