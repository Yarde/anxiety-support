using UnityEngine;
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
            Application.targetFrameRate = 30;
        }
    }
}