using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSpentStateMachine : MonoBehaviour
{
    [SerializeField] private List<BaseState> _states;
    private BaseState _currentSate;

    private void Start()
    {
        ChangeState<UnitCreateState>();
    }

    public void ChangeState<T>() where T : BaseState
    {
        if (_currentSate != null)
            _currentSate.Exit();

        _currentSate = _states.FirstOrDefault(state => state is T);
        _currentSate.Enter();
    }
}
