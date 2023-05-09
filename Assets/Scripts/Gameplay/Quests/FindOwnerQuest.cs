using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using Yarde.Gameplay.Entities;
using Yarde.Gameplay.Entities.Entity;
using Yarde.Light;
using Yarde.Quests;

namespace Yarde.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "FindOwnerQuest", menuName = "Quests/FindOwnerQuest", order = 2)]
    public class FindOwnerQuest : Quest
    {
        [SerializeField] private float _timeToFinish;

        [Inject] [UsedImplicitly] private EntityManager _entityManager;
        [Inject] [UsedImplicitly] private EffectManager _effectManager;
        
        private Dog _dog;
        private Owner _owner;

        private float Distance => (_dog.View.transform.position - _owner.View.transform.position).magnitude;

        protected override void RunInternal()
        {
            _dog = _entityManager.GetEntityByType<Dog>();
            _owner = _entityManager.GetEntityByType<Owner>();
        }

        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            AdjustLight(cts).Forget();
            await UniTask.WaitUntil(() => Distance < 2, cancellationToken: cts.Token);
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            bool LoosCondition()
            {
                var outOfTime = (_timeToFinish -= Time.deltaTime) <= 0;
                var tooFar = Distance > 40;
                return outOfTime || tooFar;
            }
            
            await UniTask.WaitUntil(LoosCondition, cancellationToken: cts.Token);
        }
        
        private async UniTaskVoid AdjustLight(CancellationTokenSource cts)
        {
            while (!cts.IsCancellationRequested)
            {
                var state = Mathf.InverseLerp(40f, 15f, Distance);
                _effectManager.SetIntensity(state);
                await UniTask.Delay(100);
            }
        }
    }
}