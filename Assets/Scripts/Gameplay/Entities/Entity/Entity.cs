using VContainer;
using VContainer.Unity;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.Entity
{
    public abstract class Entity
    {
        public EntityView View { get; private set; }

        private readonly IObjectResolver _container;

        protected Entity(IObjectResolver container)
        {
            _container = container;
        }

        public void Setup(SpawnPoint.SpawnPoint spawnPoint)
        {
            SpawnEntityViewFromSpawnPoint(_container, spawnPoint);
            SetupInternal();
        }

        protected abstract void SetupInternal();

        private void SpawnEntityViewFromSpawnPoint(IObjectResolver container, SpawnPoint.SpawnPoint spawnPoint)
        {
            View = container.Instantiate(spawnPoint.Prefab, spawnPoint.Transform);
        }
    }
}