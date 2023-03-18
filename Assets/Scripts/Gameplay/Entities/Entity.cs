using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Yarde.Gameplay.Entities
{
    public abstract class Entity
    {
        protected EntityView View { get; private set; }

        private readonly IObjectResolver _container;

        protected Entity(IObjectResolver container)
        {
            _container = container;
        }

        public void Setup(SpawnPoint spawnPoint)
        {
            SpawnEntityViewFromSpawnPoint(_container, spawnPoint);
            SetupInternal();
        }

        protected abstract void SetupInternal();

        private void SpawnEntityViewFromSpawnPoint(IObjectResolver container, SpawnPoint spawnPoint)
        {
            View = container.Instantiate(spawnPoint.Prefab, spawnPoint.Position, Quaternion.identity, spawnPoint.Parent);
        }
    }
}