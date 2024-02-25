using TowerBuildSystem;
using UnityEngine;

public class BuildTowerState : BaseState
{
    [SerializeField] private TowerBuilder _towerBuilder;

    protected override void Spend()
    {
        _towerBuilder.Build();
    }
}