using Srcipts.Unit;
using System;
using TowerBuildSystem;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private ResourceScannerView _resourceScannerView;
    [SerializeField] private BuildState _buildState;
    [SerializeField] private UnitFactory _unitFactory;
    [SerializeField] private SquadView _squadView;

    [field: SerializeField] public Transform SquadTransform { get; private set; }

    public void Initialize(GestureClick tirrain, CoroutineServise coroutineServise, TowerFactory towerFactory)
    {
        if (tirrain == null)
            throw new ArgumentNullException();

        if (coroutineServise == null)
            throw new ArgumentNullException();

        if (towerFactory == null)
            throw new ArgumentNullException();

        _resourceScannerView.Initialize(coroutineServise);
        _buildState.Initialize(tirrain);
        _unitFactory.Initialize(towerFactory);
        _squadView.Initialize();
    }

    private void OnEnable()
    {
        _resourceScannerView.Enable();
        _squadView.Enable();
    }

    private void OnDisable()
    {
        _resourceScannerView.Disable();
        _squadView.Disable();
    }

    public void Add(UnitView unit)
    {
        _squadView.Add(unit);
    }
}