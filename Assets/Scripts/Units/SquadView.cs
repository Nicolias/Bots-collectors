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

    private List<UnitView> _units = new List<UnitView>();

    private void Awake()
    {
        _units.AddRange(_unitsFactory.Create(_startUnitsCount));
    }

    private void OnEnable()
    {
        _units.ForEach(unit => unit.Mined += OnMined);
    }

    private void OnDisable()
    {
        _units.ForEach(unit => unit.Mined -= OnMined);
    }

    public void Add(UnitView unit)
    {
        if (unit == null)
            throw new ArgumentNullException();

        _units.Add(unit);
    }

    public void Mine(ResourceView resource)
    {
        if (resource == null)
            throw new ArgumentNullException();

        if (resource.IsMining)
            throw new InvalidOperationException();

        List<UnitView> freeUnits = _units.Where(unit => unit.IsMining == false).ToList();

        if (freeUnits.Count > 0)
            freeUnits.First().Mine(resource);
    }

    private void OnMined(ResourceView resource)
    {
        _repository.Add();
    }
}
