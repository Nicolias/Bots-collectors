using System;
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
            IsMining = false;
            Mined?.Invoke(_currentResource);
        }
    }
}