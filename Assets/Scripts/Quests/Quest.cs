using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yarde.Quests
{
    public abstract class Quest : ScriptableObject
    {
        public event Action OnSucceeded;
        public event Action OnFailed;

        private CancellationTokenSource _cancellationTokenSource;

        private void OnDestroy()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
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
            await UniTask.WhenAny(
                Success(_cancellationTokenSource), 
                Fail(_cancellationTokenSource)
                ).SuppressCancellationThrow();
        }

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
}