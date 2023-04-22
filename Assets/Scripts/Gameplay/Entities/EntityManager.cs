using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using Yarde.Camera;
using Yarde.Gameplay.Entities.Entity;
using Yarde.Gameplay.Entities.SpawnPoints;
using Object = UnityEngine.Object;

namespace Yarde.Gameplay.Entities
{
    [UsedImplicitly]
    public class EntityManager : IDisposable
    {
        private CancellationTokenSource _cts;
        
        private readonly List<Entity.Entity> _entities;
        private readonly IObjectResolver _container;

        public EntityManager(IObjectResolver container, CameraManager cameraManager)
        {
            _container = container;
            _entities = new List<Entity.Entity>();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _entities.Clear();
        }

        public T GetEntityByType<T>() where T : Entity.Entity
        {
            return _entities.FirstOrDefault(e => e.GetType() == typeof(T)) as T;
        }

        public void AttackEntity(GameObject hit, int damage)
        {
            var entity = _entities.FirstOrDefault(e => e.View.gameObject == hit);
            if (entity != null && entity.TakeDamage(damage))
            {
                _entities.Remove(entity);
            }
        }

        public void Setup()
        {
            _cts = new CancellationTokenSource();
            SpawnAllEntities();
        }

        private void SpawnAllEntities()
        {
            var spawnPoints = Object.FindObjectsOfType<SpawnPoint>();
            foreach (var spawnPoint in spawnPoints)
            {
                if (spawnPoint.Delay > 0)
                {
                    SpawnDelayedEntity(spawnPoint, spawnPoint.Delay).Forget();
                }
                else
                {
                    _entities.Add(SpawnEntity(spawnPoint));
                }
            }

            foreach (var entity in _entities)
            {
                entity.Start();
            }
        }

        private async UniTaskVoid SpawnDelayedEntity(SpawnPoint spawnPoint, float delay)
        {
            var cancelled = await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: _cts.Token)
                .SuppressCancellationThrow();
            if (cancelled)
            {
                return;
            }
            var entity = SpawnEntity(spawnPoint);
            _entities.Add(entity);
            entity.Start();
        }

        private Entity.Entity SpawnEntity(SpawnPoint spawnPoint)
        {
            var entity = CreateEntity(spawnPoint.Type, _container, spawnPoint);
            entity.Awake();
            
            spawnPoint.Repeats--;
            if (spawnPoint.Repeats > 0)
            {
                SpawnDelayedEntity(spawnPoint, spawnPoint.Cooldown).Forget();
            }
            
            return entity;
        }

        private static Entity.Entity CreateEntity(EntityType type, IObjectResolver container, SpawnPoint spawnPoint)
        {
            return type switch
            {
                EntityType.Dog => new Dog(container, spawnPoint),
                EntityType.Human => new Human(container, spawnPoint),
                EntityType.Owner => new Owner(container, spawnPoint),
                EntityType.Monster => new Monster(container, spawnPoint),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}