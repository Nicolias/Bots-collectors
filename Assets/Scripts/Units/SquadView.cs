using Srcipts.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SquadView : MonoBehaviour
{
    [SerializeField] private int _startUnitsCount;
    [SerializeField] private ResourceRepository _repository;

    [SerializeField] private UnitFactory _unitsFactory;

    private List<UnitView> _units;
    private Queue<UnitView> _freeUnits;

    public void Initialize()
    {
        _units = new List<UnitView>(_unitsFactory.Create(_startUnitsCount));
        _freeUnits = new Queue<UnitView>(_units);
    }

    public void Enable()
    {
        _units.ForEach(unit => unit.Mined += OnMined);
        _units.ForEach(unit => unit.Freed += AddToFree);
    }

    public void Disable()
    {
        _units.ForEach(unit => unit.Mined -= OnMined);
        _units.ForEach(unit => unit.Freed -= AddToFree);
    }

    public void Add(UnitView unit)
    {
        if (unit == null)
            throw new ArgumentNullException();

        _units.Add(unit);
        unit.Freed += AddToFree;
        unit.Mined += OnMined;

        if (unit.IsBusy == false)
            _freeUnits.Enqueue(unit);
    }

    public void Remove(UnitView unit)
    {
        if (unit == null)
            throw new ArgumentNullException();

        _units.Remove(unit);
        unit.Freed -= AddToFree;
        unit.Mined -= OnMined;
    }

    public bool TryGetFreeUnit(out UnitView unit)
    {
        if (_freeUnits.Count > 0)
            unit = _freeUnits.Dequeue();
        else
            unit = null;

        return unit != null;
    }

    private void OnMined(ResourceView resource)
    {
        _repository.Add();
    }

    private void AddToFree(UnitView unit)
    {
        _freeUnits.Enqueue(unit);
    }
}
