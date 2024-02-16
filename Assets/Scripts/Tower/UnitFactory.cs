using Srcipts.Unit;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField] private UnitView _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _towerTransform;

    public UnitView Create()
    {
        UnitView newUnit = Instantiate(_prefab, _container);
        newUnit.Initialize(_towerTransform);

        return newUnit;
    }

    public IEnumerable<UnitView> Create(int count)
    {
        List<UnitView> units = new List<UnitView>();

        for (int i = 0; i < count; i++)
            units.Add(Create());

        float angelStep = 360 / units.Count * Mathf.Deg2Rad;
        float radious = 1;

        for (int i = 0; i < units.Count; i++)
        {
            float angel = angelStep * i;
            Vector2 locolPosition = new Vector2(Mathf.Cos(angel), Mathf.Sin(angel)) * radious;

            units[i].transform.position = new Vector3(_towerTransform.position.x + locolPosition.x,
                units[i].transform.position.y,
                _towerTransform.position.z + locolPosition.y);
        }

        return units;
    }
}