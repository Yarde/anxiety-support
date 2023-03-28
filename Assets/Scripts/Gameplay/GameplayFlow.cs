using System.Collections.Generic;
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
                await SceneManager.UnloadSceneAsync(_currentSceneIndex);
                _currentSceneIndex = 0;
            }

            _currentSceneIndex++;
            
            Debug.Log($"Loading scene {_currentSceneIndex}");
            if (_currentSceneIndex > 1)
            {
                await SceneManager.UnloadSceneAsync(_currentSceneIndex - 1);
            }
            await SceneManager.LoadSceneAsync(_currentSceneIndex, LoadSceneMode.Additive);
            StartQuest();
        }

        private async void RestartScene()
        {
            Debug.Log($"Restarting scene {_currentSceneIndex}");
            await SceneManager.UnloadSceneAsync(_currentSceneIndex);
            await SceneManager.LoadSceneAsync(_currentSceneIndex, LoadSceneMode.Additive);
            StartQuest();
        }

        private void StartQuest()
        {
            var quest = _questline.GetQuest(_currentSceneIndex - 1);
            
            // todo find better solution
            var context = Object.FindObjectOfType<GameplayContext>();
            var container = context.Container;
            var scene = container.Resolve<GameplayScene>();
            scene.Start(quest, LoadNextScene, RestartScene);
        }
    }
}