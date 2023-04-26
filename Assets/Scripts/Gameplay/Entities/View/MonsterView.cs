using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Yarde.Utils.Extensions;

namespace Yarde.Gameplay.Entities.View
{
    public class MonsterView : EntityView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        public bool IsAttackingDistance => _navMeshAgent.hasPath &&
                                           _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;

        public void SetTarget(EntityView target)
        {
            _navMeshAgent.stoppingDistance = Random.Range(3f, 5f);
            _navMeshAgent.SetDestination(target.transform.position);
        }

        public async UniTaskVoid OnDie()
        {
            _characterController.enabled = false;
            await _animator.TriggerAndWaitForStateEnd("Die", this.GetCancellationTokenOnDestroy());
            Destroy(gameObject);
        }

        public async UniTask Attack()
        {
            await _animator.TriggerAndWaitForStateEnd("Attack", this.GetCancellationTokenOnDestroy());
        }
    }
}