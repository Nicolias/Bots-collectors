using System;
using UnityEngine;

namespace TowerBuildSystem
{
    public class BuildState : BaseState
    {
        [SerializeField] private TowerBuilder _towerBuilder;
        [SerializeField] private PlayerClickStateMachine _stateMachine;

        private GestureClick _tirrain;

        public void Initialize(GestureClick tirrain)
        {
            if (tirrain == null)
                throw new ArgumentNullException();

            _tirrain = tirrain;
        }

        public override void Enter()
        {
            ResourceSpentStateMachine.ChangeState<BuildTowerState>();
            _tirrain.Click += OnClick;
            _towerBuilder.Built += OnBuilt;
        }

        public override void Exit()
        {
            _tirrain.Click -= OnClick;
            _towerBuilder.Built -= OnBuilt;
        }

        private void OnClick(Vector3 position)
        {
            _towerBuilder.SetBuildPoint(position);
        }

        private void OnBuilt()
        {
            _stateMachine.ChangeState<IdleState>();
        }
    }
}