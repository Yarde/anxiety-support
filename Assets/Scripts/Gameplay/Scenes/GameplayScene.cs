using System;
using JetBrains.Annotations;
using Yarde.Camera;
using Yarde.Gameplay.Entities;
using Yarde.Gameplay.Entities.Entity;
using Yarde.Quests;

namespace Yarde.Gameplay.Scenes
{
    [UsedImplicitly]
    public class GameplayScene : IScene
    {
        private readonly QuestSystem _questSystem;
        private readonly EntityManager _entityManager;
        private readonly CameraManager _cameraManager;

        public GameplayScene(QuestSystem questSystem, EntityManager entityManager, CameraManager cameraManager)
        {
            _questSystem = questSystem;
            _entityManager = entityManager;
            _cameraManager = cameraManager;
        }

        public void Start(string questId, Action onSuccess, Action onFail)
        {
            _entityManager.Setup();
            _cameraManager.SelectTarget(_entityManager.GetEntityByType(typeof(Dog))?.View.transform);
            _questSystem.StartQuest(questId, onSuccess, onFail);
        }
    }
}