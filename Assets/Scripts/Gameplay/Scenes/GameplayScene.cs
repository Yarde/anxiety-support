using JetBrains.Annotations;
using VContainer;
using Yarde.Camera;
using Yarde.Gameplay.Entities;
using Yarde.Gameplay.Entities.Entity;
using Yarde.Quests;
using Yarde.UI;

namespace Yarde.Gameplay.Scenes
{
    [UsedImplicitly]
    public class GameplayScene : BaseScene
    {
        private readonly EntityManager _entityManager;
        private readonly CameraManager _cameraManager;

        public GameplayScene(GameplayFlow gameplayFlow, QuestSystem questSystem, EntityManager entityManager,
            CameraManager cameraManager, CanvasManager canvasManager) : base(gameplayFlow, questSystem, canvasManager)
        {
            _entityManager = entityManager;
            _cameraManager = cameraManager;
        }


        protected override void InternalStart()
        {
            _entityManager.Setup();
            _cameraManager.SelectTarget(_entityManager.GetEntityByType<Dog>()?.View.transform);
        }
    }
}