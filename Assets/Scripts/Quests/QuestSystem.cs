using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Yarde.Quests
{
    public class QuestSystem : IDisposable
    {
        private List<Quest> _activeQuests = new();
        private IObjectResolver _container;

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
            quest.Run().Forget();

            quest.OnSucceeded += onSuccess;
            quest.OnFailed += onFail;

            void OnQuestFinished()
            {
                _activeQuests.Remove(quest);
                Object.Destroy(quest);
            }

            quest.OnSucceeded += OnQuestFinished;
            quest.OnFailed += OnQuestFinished;

            _activeQuests.Add(quest);
        }

        private Quest LoadQuestData(string questId)
        {
            return Resources.Load<Quest>(questId);
        }
    }
}