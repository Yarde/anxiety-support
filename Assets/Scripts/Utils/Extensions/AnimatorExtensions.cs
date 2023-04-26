using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yarde.Utils.Extensions
{
    public static class AnimatorExtensions
    {
        public static UniTask WaitForState(this Animator animator, string stateName, CancellationToken ctx)
        {
            return UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName(stateName), cancellationToken: ctx);
        }
        
        public static UniTask WaitForStateChange(this Animator animator, string stateName, CancellationToken ctx)
        {
            return UniTask.WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(stateName), cancellationToken: ctx);
        }

        public static UniTask WaitForStateChange(this Animator animator, CancellationToken ctx)
        {
            var name = animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            return UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).shortNameHash != name,
                cancellationToken: ctx);
        }

        public static UniTask WaitForStateEnd(this Animator animator, CancellationToken ctx)
        {
            return UniTask.WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.999f,
                cancellationToken: ctx);
        }

        public static async UniTask WaitForChangeAndEnd(this Animator animator, string stateName,
            CancellationToken ctx)
        {
            if (await animator.WaitForState(stateName, ctx).SuppressCancellationThrow())
            {
                return;
            }
            await animator.WaitForStateChange(ctx);
        }
        
        public static UniTask TriggerAndWaitForStateEnd(this Animator animator, string stateName,
            CancellationToken ctx)
        {
            animator.SetTrigger(stateName);
            return animator.WaitForChangeAndEnd(stateName, ctx);
        }
    }
}