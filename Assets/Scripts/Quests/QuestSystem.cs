using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Yarde.Quests
{
    [UsedImplicitly]
    public class QuestSystem : IDisposable
    {
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

            SubscribeToQuest(onSuccess, onFail, quest);
            quest.Run().Forget();

            _activeQuests.Add(quest);
        }

        private void SubscribeToQuest(Action onSuccess, Action onFail, Quest quest)
        {
            quest.OnSucceeded += onSuccess;
            quest.OnFailed += onFail;

            void OnQuestFinished()
            {
                _activeQuests.Remove(quest);
                Object.Destroy(quest);
            }

            quest.OnSucceeded += OnQuestFinished;
            quest.OnFailed += OnQuestFinished;
        }

        private Quest LoadQuestData(string questId)
        {
            return Resources.Load<Quest>(questId);
        }
    }
}