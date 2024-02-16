using UnityEngine;

public class UnitCreator : MonoBehaviour
{
    [SerializeField] private UnitFactory _unitFactory;

    [SerializeField] private ResourceRepository _resourceRepository;
    [SerializeField] private int _resourceConditionForCreate;
    [SerializeField] private SquadView _squad;

    private void OnEnable()
    {
        _resourceRepository.Changed += OnResourceCountChanged;
    }

    private void OnDisable()
    {
        _resourceRepository.Changed -= OnResourceCountChanged;
    }

    private void OnResourceCountChanged()
    {
        if (_resourceRepository.Count >= _resourceConditionForCreate)
        {
            _resourceRepository.Decrease(_resourceConditionForCreate);
            _squad.Add(_unitFactory.Create());
        }
    }
}
