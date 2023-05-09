using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using VContainer;
using Yarde.Audio;
using Yarde.Quests;
using Yarde.Scene;

namespace Yarde.Gameplay
{
    [UsedImplicitly]
    public class GameplayFlow
    {
        [Inject] private AudioManager _audioManager;
        
        private readonly Questline _questline;
        private readonly SceneController _sceneController;

        private int _currentQuestIndex;
        public Quest CurrentQuest { get; private set; }

        public GameplayFlow(Questline questline, SceneController sceneController)
        {
            _questline = questline;
            _sceneController = sceneController;
        }

        public void Start()
        {
            StartQuest(_currentQuestIndex).Forget();
        }
        
        private async UniTask StartQuest(int questIndex, string sceneToUnload = null)
        {
            CurrentQuest = _questline.GetQuest(questIndex);
            _audioManager.PlayClip(AudioType.Music, CurrentQuest.Music);
            await _sceneController.ChangeScene(CurrentQuest.SceneName, sceneToUnload);
        }

        public void StartNextQuest()
        {
            if (_questline.IsLastQuest(_currentQuestIndex))
            {
                _currentQuestIndex = 0;
            }
            else
            {
                _currentQuestIndex++;
            }

            StartQuest(_currentQuestIndex, CurrentQuest.SceneName).Forget();
        }

        public void RestartQuest()
        {
            StartQuest(_currentQuestIndex, CurrentQuest.SceneName).Forget();
        }
    }
}