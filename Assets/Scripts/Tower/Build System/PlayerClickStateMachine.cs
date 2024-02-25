using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerBuildSystem
{
    public class PlayerClickStateMachine : MonoBehaviour
    {
        [SerializeField] private List<BaseState> _states;
        private BaseState _currentSate;

        private void Start()
        {
            ChangeState<IdleState>();
        }

        public void ChangeState<T>() where T : BaseState
        {
            if (_currentSate != null)
                _currentSate.Exit();

            _currentSate = _states.FirstOrDefault(state => state is T);
            _currentSate.Enter();
        }
    }
}