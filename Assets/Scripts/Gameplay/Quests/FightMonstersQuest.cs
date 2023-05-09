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
    [CreateAssetMenu(fileName = "FightMonstersQuest", menuName = "Quests/FightMonstersQuest", order = 4)]
    public class FightMonstersQuest : Quest
    {
        [Inject] [UsedImplicitly] private EntityManager _entityManager;
        [Inject] [UsedImplicitly] private EffectManager _effectManager;

        private Dog _dog;
        private Owner _owner;

        private float Distance => (_dog.View.transform.position - _owner.View.transform.position).magnitude;

        protected override void RunInternal()
        {
            _owner = _entityManager.GetEntityByType<Owner>();
            _dog = _entityManager.GetEntityByType<Dog>();
        }

        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            AdjustLight(cts).Forget();
            await UniTask.WaitWhile(() => _entityManager.AnyMonsterLeft());
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            bool LoosCondition()
            {
                var ownerDied = _owner.Health <= 0;
                var tooFar = Distance > 40;
                return ownerDied || tooFar;
            }

            await UniTask.WaitUntil(LoosCondition, cancellationToken: cts.Token);
        }

        private async UniTaskVoid AdjustLight(CancellationTokenSource cts)
        {
            while (!cts.IsCancellationRequested)
            {
                var health = _owner.Health / 100f;
                var distance = Mathf.InverseLerp(40f, 15f, Distance);
                var intensity = Mathf.Min(health, distance);
                Debug.Log(intensity);
                _effectManager.SetIntensity(intensity);
                await UniTask.Delay(100);
            }
        }
    }
}