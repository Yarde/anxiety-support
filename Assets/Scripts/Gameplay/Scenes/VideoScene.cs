using System;
using JetBrains.Annotations;
using Yarde.Quests;

namespace Yarde.Gameplay.Scenes
{
    [UsedImplicitly]
    public class VideoScene : IScene
    {
        private readonly QuestSystem _questSystem;

        public VideoScene(QuestSystem questSystem)
        {
            _questSystem = questSystem;
        }

        public void Start(string questId, Action onSuccess, Action onFail)
        {
            _questSystem.StartQuest(questId, onSuccess, onFail);
        }
    }
}