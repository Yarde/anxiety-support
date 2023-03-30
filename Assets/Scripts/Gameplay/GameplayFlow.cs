using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Yarde.Gameplay.Context;
using Yarde.Gameplay.Scenes;
using Yarde.Quests;

namespace Yarde.Gameplay
{
    [UsedImplicitly]
    public class GameplayFlow : IStartable
    {
        private readonly Questline _questline;

        private int _currentQuestIndex;
        private Quest _currentQuest;

        public GameplayFlow(Questline questline)
        {
            _questline = questline;
        }

        void IStartable.Start()
        {
            StartQuest(_currentQuestIndex).Forget();
        }

        private void StartNextQuest()
        {
            if (_questline.IsLastQuest(_currentQuestIndex))
            {
                _currentQuestIndex = 0;
            }
            else
            {
                _currentQuestIndex++;
            }

            StartQuest(_currentQuestIndex, _currentQuest.SceneName).Forget();
        }

        private void RestartQuest()
        {
            StartQuest(_currentQuestIndex, _currentQuest.SceneName).Forget();
        }

        private async UniTask StartQuest(int questIndex, string sceneToUnload = null)
        {
            _currentQuest = _questline.GetQuest(questIndex);

            if (!string.IsNullOrEmpty(sceneToUnload))
            {
                Debug.Log($"Unloading scene {sceneToUnload}");
                await SceneManager.UnloadSceneAsync(sceneToUnload);
            }

            Debug.Log($"Loading scene {_currentQuest.SceneName}");
            await SceneManager.LoadSceneAsync(_currentQuest.SceneName, LoadSceneMode.Additive);

            // todo find better solution
            var context = Object.FindObjectOfType<SceneContext>();
            var container = context.Container;
            var scene = container.Resolve<IScene>();
            scene.Start(_currentQuest.name, StartNextQuest, RestartQuest);
        }
    }
}