using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScannerView : MonoBehaviour
{
    [SerializeField] private LayerMask _resourcesLayer;
    [SerializeField] private float _radius;

    [SerializeField] private float _delay;

    private ResourceScannerPresenter _presenter;

    public event Action<IEnumerable<ResourceView>> ResourcesDetected;

    public void Initialize(CoroutineServise coroutineServise)
    {
        _presenter = new ResourceScannerPresenter(coroutineServise, this, _delay);
    }

    public void Enable()
    {
        _presenter.Enable();
    }

    public void Disable()
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

        if (resourcesForMine.Count > 0)
            ResourcesDetected?.Invoke(resourcesForMine);
    }
}
