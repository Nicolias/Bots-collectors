using UnityEngine;

namespace TowerBuildSystem
{
    public abstract class BaseState : MonoBehaviour
    {
        [field: SerializeField] protected ResourceSpentStateMachine ResourceSpentStateMachine { get; private set; }

        public abstract void Enter();
        public abstract void Exit();
    }
}