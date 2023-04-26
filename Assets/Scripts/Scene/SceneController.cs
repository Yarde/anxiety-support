using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Yarde.Scene
{
    public class SceneController
    {
        private const string LoadingSceneName = "LoadingScene";
        private const int MinAnimationTimeWithAnimation = 5;

        private LoadingScreen _loadingScreen;

        private readonly List<string> _scenesWithAnimation = new()
        {
            "FindOwnerScene"
        };

        private async UniTask<LoadingScreen> Initialize()
        {
            await SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
            return Object.FindObjectOfType<LoadingScreen>();
        }

        public async UniTask ChangeScene(string sceneToLoad, string sceneToUnload)
        {
            _loadingScreen ??= await Initialize();
            var showAnimation = _scenesWithAnimation.Contains(sceneToLoad);

            var task = _loadingScreen.StartLoading(showAnimation);

            if (!string.IsNullOrEmpty(sceneToUnload))
            {
                Debug.Log($"Unloading scene {sceneToUnload}");
                await SceneManager.UnloadSceneAsync(sceneToUnload);
            }

            Debug.Log($"Loading scene {sceneToLoad}");
            await UniTask.WhenAll(
                task,
                SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive).ToUniTask(),
                showAnimation ?
                UniTask.Delay(TimeSpan.FromSeconds(MinAnimationTimeWithAnimation))
                : UniTask.CompletedTask);

            await _loadingScreen.StopLoading(showAnimation);
        }
    }
}