using System;
using UnityEngine;

namespace Srcipts.Unit
{
    public class UnitBuildPresenter : MonoBehaviour
    {
        [SerializeField] private MoveState _moveState;

        private TowerFactory _towerFactory;

        private Vector3 _currentBuildPosition;

        public event Action<Tower> Built;

        public void Initialize(TowerFactory towerFactory)
        {
            if (towerFactory == null)
                throw new ArgumentNullException();
            
            _towerFactory = towerFactory;
        }

        public void Build(Vector3 position)
        {
            _currentBuildPosition = position;
            _moveState.Reached += OnBuildPositionReached;

            _moveState.Enable();
            _moveState.MoveTo(position);
        }

        private void OnBuildPositionReached()
        {
            _moveState.Reached -= OnBuildPositionReached;
            _moveState.Disable();

            Tower newTower = _towerFactory.Create(_currentBuildPosition);
            Built?.Invoke(newTower);
        }
    }
}