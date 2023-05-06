using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Yarde.Quests
{
    [UsedImplicitly]
    public class QuestSystem : IDisposable
    {
        private const string QuestsPath = "Quests/";

        private readonly List<Quest> _activeQuests = new();
        private readonly IObjectResolver _container;

        public QuestSystem(IObjectResolver container)
        {
            _container = container;
        }

        public void Dispose()
        {
            foreach (var quest in _activeQuests)
            {
                Object.Destroy(quest);
            }

            _activeQuests.Clear();
        }

        public void StartQuest(string questId, Action onSuccess, Action onFail)
        {
            var questData = LoadQuestData(questId);
            var quest = _container.Instantiate(questData);

            SubscribeToQuest(questId, onSuccess, onFail, quest);
            Debug.Log($"Starting quest {questId}");
            quest.Run().Forget();

            _activeQuests.Add(quest);
        }

        private void SubscribeToQuest(string questId, Action onSuccess, Action onFail, Quest quest)
        {
            void OnQuestFinished(string questSucceeded)
            {
                Debug.Log(questSucceeded);
                _activeQuests.Remove(quest);
                Assert.IsFalse(_activeQuests.Contains(quest),
                    "two quests with the same id are active wrong one could be removed");
                Object.Destroy(quest);
            }

            quest.OnSucceeded += () => OnQuestFinished($"Quest {questId} succeeded");
            quest.OnFailed += () => OnQuestFinished($"Quest {questId} failed");

            quest.OnSucceeded += onSuccess;
            quest.OnFailed += onFail;
        }

        private Quest LoadQuestData(string questId)
        {
            return Resources.Load<Quest>(QuestsPath + questId);
        }
    }
}