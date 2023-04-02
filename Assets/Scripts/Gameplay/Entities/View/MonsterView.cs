using UnityEngine;
using UnityEngine.AI;

namespace Yarde.Gameplay.Entities.View
{
    public class MonsterView : EntityView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public void SetTarget(EntityView target)
        {
            GoToTarget(target);
        }

        private void GoToTarget(EntityView target)
        {
            _navMeshAgent.stoppingDistance = Random.Range(4f, 7f);
            _navMeshAgent.SetDestination(target.transform.position);
        }
    }
}