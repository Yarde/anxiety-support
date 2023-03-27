using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Yarde.Quests;

namespace Yarde.Gameplay
{
    [UsedImplicitly]
    public class GameplayFlow : IStartable
    {
        private readonly IObjectResolver _container;
        private readonly Questline _questline;

        private int _currentSceneIndex;

        public GameplayFlow(IObjectResolver container, Questline questline)
        {
            _container = container;
            _questline = questline;
        }

        void IStartable.Start()
        {
            LoadNextScene();
        }

        // todo fix cancellation
        private async void LoadNextScene()
        {
            if (_questline.IsLastQuest(_currentSceneIndex - 1))
            {
                _currentSceneIndex = 0;
            }
            
            await LoadScene(++_currentSceneIndex);
            StartQuest();
        }

        private async void RestartScene()
        {
            await LoadScene(_currentSceneIndex);
            StartQuest();
        }

        private void StartQuest()
        {
            var quest = _questline.GetQuest(_currentSceneIndex - 1);
            
            // todo find better solution
            Object.FindObjectOfType<GameplayContext>().Container.Resolve<GameplayScene>().Start(quest, LoadNextScene, RestartScene);
        }

        private async UniTask LoadScene(int index)
        {
            // todo add loading screen
            if (SceneManager.GetSceneByBuildIndex(index).isLoaded)
            {
                await SceneManager.UnloadSceneAsync(index);
            }

            await SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        }
    }
}