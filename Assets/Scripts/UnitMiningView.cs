using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Srcipts.Unit
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private UnitMinePresenter _minePresenter;

        private ResourceView _currentResource;

        public bool IsMining { get; private set; }

        public event Action<ResourceView> Mined;

        private void OnEnable()
        {
            _minePresenter.Mined += OnMined;            
        }

        private void OnDisable()
        {
            _minePresenter.Mined -= OnMined;
        }

        public void Mine(ResourceView resource)
        {
            if (resource == null)
                throw new ArgumentNullException();

            if (IsMining)
                throw new InvalidOperationException();

            _currentResource = resource;
            IsMining = true;

            resource.SelectForMine();
            _minePresenter.Mine(resource);
        }

        private void OnMined()
        {
            Mined?.Invoke(_currentResource);
        }
    }

    public class UnitMiningView : MonoBehaviour
    {
        [SerializeField] private float _mineDuration;

        private bool _isMining;

        public event Action Mined;

        public void Mine(ResourceView resource)
        {
            if (resource == null)
                throw new ArgumentNullException();

            if (_isMining)
                throw new InvalidOperationException();

            _isMining = true;

            StartCoroutine(StartMine(resource));
        }

        private IEnumerator StartMine(ResourceView resource)
        {
            yield return new WaitForSeconds(_mineDuration);

            resource.PickUp();
            _isMining = false;
            Mined?.Invoke();
        }
    }

    [RequireComponent(typeof(CharacterController))]
    public class UnitMover : MonoBehaviour
    {
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Enable()
        {
            _characterController.enabled = true;
        }

        public void Disable()
        {
            _characterController.enabled = false;
        }

        public void MoveTo(Vector3 target)
        {
            _characterController.Move(target);
        }
    }

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

    public class MineState : MonoBehaviour
    {
        [SerializeField] private UnitMiningView _view;

        public event Action Mined;

        public void Enable()
        {
            _view.Mined += OnMined;
        }

        public void Disable()
        {
            _view.Mined -= OnMined;
        }

        public void Mine(ResourceView resourceView)
        {
            _view.Mine(resourceView);
        }

        private void OnMined()
        {
            Mined?.Invoke();
        }
    }
}