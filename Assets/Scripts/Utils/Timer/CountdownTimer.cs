using System;
using TMPro;
using UnityEngine;

namespace Yarde.Utils.Timer
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private string _format = "mm\\:ss";
        [SerializeField] private string _outputText = "{0}";

        private TextMeshProUGUI _timer;
        private bool _initialized;
        private TimeSpan TimeLeft => _endTime.Subtract(TimeSpan.FromSeconds(Time.realtimeSinceStartup));
        private TimeSpan _endTime;

        public float TimeInSeconds
        {
            set
            {
                _initialized = true;
                _endTime = TimeSpan.FromSeconds(Time.realtimeSinceStartup + value);
            }
        }

        private void Awake()
        {
            _timer = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (!_initialized)
                return;

            var timeLeft = TimeLeft;
            if (timeLeft.Milliseconds < 0)
            {
                _initialized = false;
                timeLeft = TimeSpan.Zero;
            }

            _timer.text = string.Format(_outputText, timeLeft.ToString(_format));
        }
    }
}