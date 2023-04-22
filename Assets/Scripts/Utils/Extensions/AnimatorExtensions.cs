using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yarde.Utils.Extensions
{
    public static class AnimatorExtensions
    {
        public static UniTask WaitForState(this Animator animator, string stateName)
        {
            return UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(stateName));
        }

        public static UniTask WaitForStateChange(this Animator animator, CancellationToken ctx)
        {
            var name = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            return UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).shortNameHash != name,
                cancellationToken: ctx);
        }

        public static UniTask WaitForStateEnd(this Animator animator, CancellationToken ctx)
        {
            return UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1,
                cancellationToken: ctx);
        }

        public static async UniTask WaitForChangeAndEnd(this Animator animator, CancellationToken ctx)
        {
            await animator.WaitForStateChange(ctx);
            await animator.WaitForStateEnd(ctx);
        }

        public static UniTask TriggerAndWaitForStateEnd(this Animator animator, string triggerName,
            CancellationToken ctx)
        {
            animator.SetTrigger(triggerName);
            return animator.WaitForChangeAndEnd(ctx);
        }
    }
}