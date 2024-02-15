using Srcipts.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SquadView : MonoBehaviour
{
    [SerializeField] private List<UnitView> _units;

    [SerializeField] private ResourceRepository _repository;

    private void OnEnable()
    {
        _units.ForEach(unit => unit.Mined += OnMined);
    }

    private void OnDisable()
    {
        _units.ForEach(unit => unit.Mined -= OnMined);
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
        _repository.Add(resource);
    }
}
