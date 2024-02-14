using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectionHandler : MonoBehaviour
{
    [SerializeField] private ResourceScannerView _scanner;

    [SerializeField] private MiningSquadView _squad;

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
