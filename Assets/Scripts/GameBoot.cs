using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarde.Utils.Logger;
using Logger = Yarde.Utils.Logger.Logger;

namespace Yarde
{
    public class GameBoot : MonoBehaviour
    {
        [SerializeField] private LoggerLevel _loggerLevel;

        private void Start()
        {
            Logger.Level = _loggerLevel;
            Application.targetFrameRate = 60;

            LoadScene(1).Forget();
        }

        private static async UniTaskVoid LoadScene(int index)
        {
#if UNITY_EDITOR
            if (SceneManager.GetSceneByBuildIndex(index).isLoaded)
            {
                await SceneManager.UnloadSceneAsync(index);
            }
#endif
            
            await SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        }
    }
}