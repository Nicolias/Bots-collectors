using Srcipts.Unit;
using System;
using System.Collections;
using UnityEngine;

namespace TowerBuildSystem
{
    public class TowerBuilder : MonoBehaviour
    {
        [SerializeField] private SquadView _squad;
        [SerializeField] private GameObject _flag;

        private bool _isBuilding;

        private Vector3 _currentBuildPosition;

        public event Action Built;

        public void SetBuildPoint(Vector3 position)
        {
            _currentBuildPosition = position;
            _flag.transform.position = position;
        }

        public void Build()
        {
            if (_isBuilding)
                throw new InvalidOperationException();

            StartCoroutine(StartBuilding());

            Built?.Invoke();
        }

        private IEnumerator StartBuilding()
        {
            UnitView builderUnit = null;

            while (builderUnit == null)
            {
                yield return null;

                if (_squad.TryGetFreeUnit(out UnitView unit))
                {
                    builderUnit = unit;
                    _squad.Remove(builderUnit);
                }
            }

            builderUnit.Freed += OnBuilt;
            builderUnit.BuildTower(_currentBuildPosition);
        }

        private void OnBuilt(UnitView unit)
        {
            unit.Freed -= OnBuilt;
            _isBuilding = false;
        }
    }
}
