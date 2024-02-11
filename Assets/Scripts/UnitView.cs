using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    public bool IsMining { get; private set; }

    public void Mine(ResourceView resource)
    {
        if (resource == null)
            throw new ArgumentNullException();

        if (IsMining)
            throw new InvalidOperationException();

        resource.SelectForMine();
        IsMining = true;
    }
}

public class UnitMiningPresenter
{

}

public class BaseView : MonoBehaviour
{
    
}

public class ResourceCollectionHandler : MonoBehaviour
{
    [SerializeField] private ResourceScannerView _scanner;

    private MiningSquadView _squad;

    private void OnEnable()
    {
        _scanner.ResourcesDetected += OnRecourcesDetected;
    }

    private void OnDisable()
    {
        _scanner.ResourcesDetected -= OnRecourcesDetected;
    }

    private void OnRecourcesDetected(IEnumerable<ResourceView> resources)
    {
        Queue<ResourceView> resourcesQueue = new Queue<ResourceView>(resources);

        _squad.Mine(resourcesQueue.Dequeue());
    }
}
