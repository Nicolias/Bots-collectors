using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    [SerializeField] private ResourceRepository _resourceRepository;
    [SerializeField] private int _resourceConditionForSpent;

    public virtual void Enter() 
    {
        _resourceRepository.Changed += OnChanged;
    }

    public virtual void Exit() 
    {
        _resourceRepository.Changed -= OnChanged;
    }

    protected abstract void Spend();

    private void OnChanged()
    {
        if (_resourceRepository.Count >= _resourceConditionForSpent)
        {
            _resourceRepository.Decrease(_resourceConditionForSpent);
            Spend();       
        }
    }
}
