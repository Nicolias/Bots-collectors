using UnityEngine;
using UnityEngine.AI;

namespace Srcipts.Unit
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMover : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            enabled = false;
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }

        public void MoveTo(Vector3 target)
        {
            _agent.SetDestination(target);
        }
    }
}