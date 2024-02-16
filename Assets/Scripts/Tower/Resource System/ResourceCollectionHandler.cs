﻿using System.Collections.Generic;
using UnityEngine;

public class ResourceCollectionHandler : MonoBehaviour
{
    [SerializeField] private ResourceScannerView _scanner;

    [SerializeField] private SquadView _squad;

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
        foreach (ResourceView resource in resources)
            _squad.Mine(resource);
    }
}