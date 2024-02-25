using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    [SerializeField] private UnitFactory _unitFactory;

    [SerializeField] private ResourceRepository _resourceRepository;
    [SerializeField] private SquadView _squad;

    public void CreateUnit()
    {
        _squad.Add(_unitFactory.Create());
    }
}