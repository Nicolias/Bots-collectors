using System;
using UnityEngine;

namespace Srcipts.Unit
{
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