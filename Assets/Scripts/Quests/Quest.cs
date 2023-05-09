using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yarde.Quests
{
    public abstract class Quest : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; private set; }
        [field: SerializeField] public UiTooltip SuccessText { get; private set; }
        [field: SerializeField] public UiTooltip FailText { get; private set; }
        [field: SerializeField] public AudioClip Music { get; private set; }

        public event Action OnSucceeded;
        public event Action OnFailed;

        private CancellationTokenSource _cancellationTokenSource;

        private void OnDestroy()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        public async UniTask Run()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
            _cancellationTokenSource = new CancellationTokenSource();
            RunInternal();
            await UniTask.WhenAny(
                Success(_cancellationTokenSource), 
                Fail(_cancellationTokenSource)
                ).SuppressCancellationThrow();
        }

        protected abstract void RunInternal();

        private async UniTask Success(CancellationTokenSource cts)
        {
            await SuccessCondition(cts);
            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
                OnSucceeded?.Invoke();
            }
        }
        
        private async UniTask Fail(CancellationTokenSource cts)
        {
            await FailCondition(cts);
            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
                OnFailed?.Invoke();
            }
        }

        protected abstract UniTask SuccessCondition(CancellationTokenSource cts);
        protected abstract UniTask FailCondition(CancellationTokenSource cts);
    }

    [Serializable]
    public class UiTooltip
    {
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
        public bool HasText => !string.IsNullOrEmpty(Text);
    }
}