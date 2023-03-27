using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Yarde.Quests
{
    public class QuestSystem
    {
        private List<Quest> _activeQuests = new();
        private IObjectResolver _container;

        public QuestSystem(IObjectResolver container)
        {
            _container = container;
        }
        
        public void StartQuest(string questId, Action onSuccess, Action onFail)
        {
            var questData = LoadQuestData(questId);
            var quest = _container.Instantiate(questData);
            quest.Run().Forget();
            
            quest.OnSucceeded += onSuccess;
            quest.OnFailed += onFail;
            _activeQuests.Add(quest);
        }

        private Quest LoadQuestData(string questId)
        {
            return Resources.Load<Quest>(questId);
        }
    }
}