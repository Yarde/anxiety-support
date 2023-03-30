using System;
using JetBrains.Annotations;
using Yarde.Gameplay.Entities;
using Yarde.Quests;

namespace Yarde.Gameplay.Scenes
{
    [UsedImplicitly]
    public class GameplayScene : IScene
    {
        private readonly QuestSystem _questSystem;
        private readonly EntityManager _entityManager;

        public GameplayScene(QuestSystem questSystem, EntityManager entityManager)
        {
            _questSystem = questSystem;
            _entityManager = entityManager;
        }

        public void Start(string questId, Action onSuccess, Action onFail)
        {
            _entityManager.Setup();
            _questSystem.StartQuest(questId, onSuccess, onFail);
        }
    }
}