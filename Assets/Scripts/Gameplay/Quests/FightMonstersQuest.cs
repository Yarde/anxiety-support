using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Yarde.Quests;

namespace Yarde.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "FightMonstersQuest", menuName = "Quests/FightMonstersQuest", order = 4)]
    public class FightMonstersQuest : Quest
    {
        protected override async UniTask SuccessCondition(CancellationTokenSource cts)
        {
            await UniTask.Delay(12000);
        }

        protected override async UniTask FailCondition(CancellationTokenSource cts)
        {
            await UniTask.Never(cts.Token);
        }
    }
}