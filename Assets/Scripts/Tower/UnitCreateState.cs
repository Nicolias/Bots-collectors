using UnityEngine;

public class UnitCreateState : BaseState
{
    [SerializeField] private UnitCreator _unitCreator;

    protected override void Spend()
    {
        _unitCreator.CreateUnit();
    }
}
