using System;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;
using Yarde.Gameplay.Entities;
using Yarde.Quests;

namespace Yarde.Gameplay
{
    [UsedImplicitly]
    public class GameplayScene
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