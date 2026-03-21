using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _states;
    public BaseState CurrentState { get; private set; }

    [SerializeField] private string currentStateName;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        _states = states;
    }

    private void Update()
    {
        if (CurrentState == null && _states != null)
        {
            SwitchToNewState(_states.Keys.First());
        }

        Type nextStateType = CurrentState?.Tick();

        if (nextStateType != null && nextStateType != CurrentState.GetType())
        {
            SwitchToNewState(nextStateType);
        }
    }

    public void SwitchToNewState(Type nextState)
    {
        CurrentState?.OnExit(_states[nextState]);
        BaseState oldState = CurrentState;

        CurrentState = _states[nextState];

        currentStateName = CurrentState.GetType().Name;

        if (oldState != null)
        {
            CurrentState.OnEnter(oldState);
        }
    }
}