using System;
using UnityEngine;

namespace Srcipts.Unit
{
    public class UnitMinePresenter : MonoBehaviour
    {
        [SerializeField] private MoveState _moveState;
        [SerializeField] private MineState _mineState;

        [SerializeField] private Transform _baseTransform;

        private ResourceView _currentResource;

        public event Action Mined;

        public void Mine(ResourceView resource)
        {
            _currentResource = resource;
            _moveState.Reached += OnResourceReached;

            _moveState.Enable();
            _moveState.MoveTo(resource.transform.position);
        }

        private void OnResourceReached()
        {
            _moveState.Reached -= OnResourceReached;
            _mineState.Mined += OnResourceMined;

            _moveState.Disable();
            _mineState.Enable();
            _mineState.Mine(_currentResource);
        }

        private void OnResourceMined()
        {
            _mineState.Mined -= OnResourceMined;
            _moveState.Reached += OnBaseReached;

            _moveState.Enable();
            _mineState.Disable();
            _moveState.MoveTo(_baseTransform.position);
        }

        private void OnBaseReached()
        {
            _moveState.Reached -= OnBaseReached;

            _moveState.Disable();
            Mined?.Invoke();
        }
    }
}