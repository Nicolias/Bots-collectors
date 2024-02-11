using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScannerView : MonoBehaviour
{
    [SerializeField] private LayerMask _resourcesLayer;
    [SerializeField] private float _radius;

    [SerializeField] private float _delay;

    [SerializeField] private CoroutineServise _coroutineServise;

    private ResourceScannerPresenter _presenter;

    public event Action<IEnumerable<ResourceView>> ResourcesDetected;

    private void Awake()
    {
        _presenter = new ResourceScannerPresenter(_coroutineServise, this, _delay);
    }

    private void OnEnable()
    {
        _presenter.Enable();
    }

    private void OnDisable()
    {
        _presenter.Disable();
    }

    public void ScaneArea()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _resourcesLayer);

        List<ResourceView> resourcesForMine = new List<ResourceView>();

        foreach (Collider hit in hits)
            if (hit.TryGetComponent(out ResourceView resource))
                if(resource.IsMining == false)
                    resourcesForMine.Add(resource);

        ResourcesDetected?.Invoke(resourcesForMine);
    }
}
