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
    }
}