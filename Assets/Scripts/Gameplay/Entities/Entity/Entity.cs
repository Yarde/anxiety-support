using VContainer;
using VContainer.Unity;
using Yarde.Gameplay.Entities.SpawnPoints;
using Yarde.Gameplay.Entities.View;

namespace Yarde.Gameplay.Entities.Entity
{
    public abstract class Entity
    {
        public EntityView View { get; private set; }

        protected readonly IObjectResolver _container;
        private readonly SpawnPoint _spawnPoint;

        protected Entity(IObjectResolver container, SpawnPoint spawnPoint)
        {
            _container = container;
            _spawnPoint = spawnPoint;
        }

        public void Awake()
        {
            SpawnEntityViewFromSpawnPoint(_container, _spawnPoint);
        }
        
        public void Start()
        {
            SetupInternal();
        }

        protected abstract void SetupInternal();

        private void SpawnEntityViewFromSpawnPoint(IObjectResolver container, SpawnPoint spawnPoint)
        {
            View = container.Instantiate(spawnPoint.Prefab, spawnPoint.Transform);
        }
    }
}