using UnityEngine;
using VContainer.Unity;
using Yarde.Gameplay.Entities;
using Yarde.Quests;

namespace Yarde.Gameplay
{
    public class GameplayBoot : IStartable
    {
        private readonly QuestSystem _questSystem;
        private readonly EntityManager _entityManager;

        public GameplayBoot(QuestSystem questSystem, EntityManager entityManager)
        {
            _questSystem = questSystem;
            _entityManager = entityManager;
        }

        void IStartable.Start()
        {
            _entityManager.Setup();
            
            _questSystem.StartQuest("find-owner",
                () => Debug.Log("success"),
                () => Debug.Log("fail"));
        }
    }
}