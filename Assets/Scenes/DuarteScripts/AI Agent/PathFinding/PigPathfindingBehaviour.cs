using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PigPathfindingBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _target;
    NavMeshAgent _agent;
    private StateMachine _stateMachine;
    private PigRuntime _pig;

    private void Awake()
    {
        _pig = GetComponent<PigRuntime>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _stateMachine = gameObject.AddComponent<StateMachine>();

        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(onHQ), new onHQ(_pig) },
            { typeof(onTravel), new onTravel(_pig, _agent) },
            { typeof(onMission), new onMission(_pig, _agent) }
        };

        _stateMachine.SetStates(states);
    }
}
