using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using Yarde.Quests;

namespace Yarde.Gameplay.Quests
{
    [CreateAssetMenu(fileName = "CreditsQuest", menuName = "Quests/CreditsQuest", order = 5)]
    public class CreditsQuest : Quest
    {
        protected override UniTask SuccessCondition(CancellationTokenSource cts)
        {
            var button = FindObjectOfType<FinishQuestButton>();
            Assert.IsNotNull(button);

            bool WaitFotUserAction()
            {
                return button.IsClicked;
            }

            return UniTask.WaitUntil(WaitFotUserAction, cancellationToken: cts.Token);
        }

        protected override UniTask FailCondition(CancellationTokenSource cts)
        {
            // nothing
            return UniTask.Never(cts.Token);
        }
    }
}