using System;
using UnityEngine;

namespace Srcipts.Unit
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private UnitBuildPresenter _buildPresenter;
        [SerializeField] private UnitMinePresenter _minePresenter;
        [SerializeField] private Rigidbody _rigidbody;

        private ResourceView _currentResource;

        public bool IsBusy { get; private set; }

        public event Action<ResourceView> Mined;
        public event Action<UnitView> Freed;

        public void Initialize(Transform towerTransform, TowerFactory towerFactory)
        {
            if (towerTransform == null)
                throw new ArgumentNullException();
            
            if (towerFactory == null)
                throw new ArgumentNullException();

            _minePresenter.Initialize(towerTransform);
            _buildPresenter.Initialize(towerFactory);
        }

        private void OnEnable()
        {
            _minePresenter.Mined += OnMined;
            _buildPresenter.Built += OnBuilt;
        }

        private void OnDisable()
        {
            _minePresenter.Mined -= OnMined;
        }

        public void Mine(ResourceView resource)
        {
            if (resource == null)
                throw new ArgumentNullException();

            if (IsBusy)
                throw new InvalidOperationException();

            _currentResource = resource;
            IsBusy = true;

            resource.SelectForMine();
            _minePresenter.Mine(resource);
        }

        public void BuildTower(Vector3 buildPosition)
        {
            if (IsBusy)
                throw new InvalidOperationException();

            IsBusy = true;

            _buildPresenter.Build(buildPosition);
        }

        private void OnMined()
        {
            IsBusy = false;
            Mined?.Invoke(_currentResource);
            Freed?.Invoke(this);
        }

        private void OnBuilt(Tower newTower)
        {
            newTower.Add(this);
            _minePresenter.Initialize(newTower.SquadTransform);

            IsBusy = false;
            Freed?.Invoke(this);
        }
    }
}