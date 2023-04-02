using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yarde.Scene
{
    public class SceneController
    {
        private const string LoadingSceneName = "LoadingScene";
        private const int MinAnimationTime = 2000;

        private LoadingScreen _loadingScreen;

        private async UniTask<LoadingScreen> Initialize()
        {
            await SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
            return Object.FindObjectOfType<LoadingScreen>();
        }

        public async UniTask ChangeScene(string sceneToLoad, string sceneToUnload)
        {
            _loadingScreen ??= await Initialize();
            await _loadingScreen.StartLoading();

            if (!string.IsNullOrEmpty(sceneToUnload))
            {
                Debug.Log($"Unloading scene {sceneToUnload}");
                await SceneManager.UnloadSceneAsync(sceneToUnload);
            }

            Debug.Log($"Loading scene {sceneToLoad}");
            await UniTask.WhenAll(
                SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive).ToUniTask(),
                UniTask.Delay(MinAnimationTime));

            await _loadingScreen.StopLoading();
        }
    }
}