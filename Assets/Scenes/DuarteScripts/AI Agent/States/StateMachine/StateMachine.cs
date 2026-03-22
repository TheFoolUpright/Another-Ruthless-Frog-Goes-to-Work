using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _states;
    public BaseState CurrentState { get; private set; }

    [SerializeField] public string currentStateName;

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
        BaseState newState = _states[nextState];

        CurrentState?.OnExit(newState);
        BaseState oldState = CurrentState;

        CurrentState = newState;
        currentStateName = CurrentState.GetType().Name;

        CurrentState.OnEnter(oldState);
    }
}