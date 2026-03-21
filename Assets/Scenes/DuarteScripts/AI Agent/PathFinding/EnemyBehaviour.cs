using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _target;
    NavMeshAgent _agent;
    private StateMachine _stateMachine;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _stateMachine = gameObject.AddComponent<StateMachine>();

        var states = new Dictionary<Type, BaseState>()
        {
            {
                typeof(onTravel), new onTravel(gameObject, _agent, _target)
            },
            {
                typeof(onMission), new onMission(gameObject, _agent)
            },
            {
                typeof(onHQ), new onHQ(gameObject, _target)
            }
        };

        _stateMachine.SetStates(states);
    }
}
