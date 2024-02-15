using System;
using UnityEngine;

namespace Srcipts.Unit
{
    public class MoveState : MonoBehaviour
    {
        [SerializeField] private UnitMover _view;
        [SerializeField] private float _minReachTargetDistance;

        private Transform _viewTransform;
        private Vector3 _currentTargetPosition;

        public event Action Reached;

        private void Awake()
        {
            _viewTransform = _view.transform;
        }

        private void Update()
        {
            if (enabled)
                if (Vector3.Distance(_viewTransform.position, _currentTargetPosition) <= _minReachTargetDistance)
                    Reached?.Invoke();
        }

        public void Enable()
        {
            _view.Enable();
            enabled = true;
        }

        public void Disable()
        {
            _view.Disable();
            enabled = false;
        }

        public void MoveTo(Vector3 target)
        {
            _currentTargetPosition = target;
            _view.MoveTo(target);
        }
    }
}