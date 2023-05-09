using VContainer.Unity;
using Yarde.Quests;
using Yarde.UI;

namespace Yarde.Gameplay.Scenes
{
    public abstract class BaseScene : IStartable
    {
        protected abstract void InternalStart();

        private readonly QuestSystem _questSystem;
        private readonly CanvasManager _canvasManager;
        private readonly GameplayFlow _gameplayFlow;

        protected BaseScene(GameplayFlow gameplayFlow, QuestSystem questSystem, CanvasManager canvasManager)
        {
            _gameplayFlow = gameplayFlow;
            _questSystem = questSystem;
            _canvasManager = canvasManager;
        }

        void IStartable.Start()
        {
            InternalStart();
            _questSystem.StartQuest(_gameplayFlow.CurrentQuest.name, OnQuestSucceed, OnQuestFailed);
        }

        private async void OnQuestSucceed()
        {
            var currentQuest = _gameplayFlow.CurrentQuest;
            await _canvasManager.ShowInfo(currentQuest.SuccessText);

            _gameplayFlow.StartNextQuest();
        }

        private async void OnQuestFailed()
        {
            var currentQuest = _gameplayFlow.CurrentQuest;
            await _canvasManager.ShowInfo(currentQuest.FailText);

            _gameplayFlow.RestartQuest();
        }
    }
}