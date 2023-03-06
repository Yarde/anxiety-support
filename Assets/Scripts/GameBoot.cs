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

            LoadScene().Forget();
        }

        private static async UniTaskVoid LoadScene()
        {
            await SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }
    }
}