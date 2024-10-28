using UnityEngine;
using UnityEngine.AI;

namespace Yonii.Unity.Utilities
{
    public static class NavMeshAgentExtensions
    {
        public static NavMeshAgent TryAndAddNavMeshAgent(this GameObject gameObject)
        {
            if (!gameObject.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
                navMeshAgent = gameObject.AddComponent<NavMeshAgent>();

            return navMeshAgent;
        }

        public static bool HasReachedDestination(
            this NavMeshAgent navMeshAgent,
            float distanceToCheck = .5f
            )
        {
            if (navMeshAgent.pathPending) 
                return false;

            if (!(navMeshAgent.remainingDistance <= distanceToCheck))
                return false;

            return navMeshAgent.velocity.sqrMagnitude < distanceToCheck;
        }
    }
}