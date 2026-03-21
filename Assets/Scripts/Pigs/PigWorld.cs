using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

public class PigWorld : MonoBehaviour
{
    private PigRuntime pig;
    private NavMeshAgent agent;
    private StateMachine stateMachine;

    public void Initialize(PigRuntime pigRuntime)
    {
        pig = pigRuntime;

        agent = GetComponent<NavMeshAgent>();
        stateMachine = gameObject.AddComponent<StateMachine>();

        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(onHQ), new onHQ(pig) },
            { typeof(onTravel), new onTravel(pig, agent) },
            { typeof(onMission), new onMission(pig, agent) }
        };

        stateMachine.SetStates(states);
    }
}