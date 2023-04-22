using UnityEngine;
using UnityEngine.AI;

namespace Yarde.Gameplay.Entities.View
{
    public class HumanView : EntityView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;

        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Update()
        {
            _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
        }

        public void SetTarget(Vector3 target)
        {
            _navMeshAgent.SetDestination(target);
        }

        public bool IsReachedTarget() => _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
    }
}