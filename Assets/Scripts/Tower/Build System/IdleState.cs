using UnityEngine;

namespace TowerBuildSystem
{
    public class IdleState : BaseState
    {
        [SerializeField] private PlayerClickStateMachine _stateMachine;
        [SerializeField] private GestureClick _tower;

        public override void Enter()
        {
            _tower.Click += OnClick;
            ResourceSpentStateMachine.ChangeState<UnitCreateState>();
        }

        public override void Exit()
        {
            _tower.Click -= OnClick;
        }

        private void OnClick(Vector3 position)
        {
            _stateMachine.ChangeState<BuildState>();
        }
    }
}