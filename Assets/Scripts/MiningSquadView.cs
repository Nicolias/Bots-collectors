using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiningSquadView : MonoBehaviour
{
    [SerializeField] private List<UnitView> _units;

    public void Mine(ResourceView resource)
    {
        if (resource == null)
            throw new ArgumentNullException();

        if (resource.IsMining)
            throw new InvalidOperationException();

        IEnumerable<UnitView> freeUnits = _units.Where(unit => unit.IsMining == false);

        foreach (UnitView freeUnit in freeUnits)
            freeUnit.Mine(resource);
    }
}