using System;
using System.Collections;
using UnityEngine;

namespace Srcipts.Unit
{
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
}