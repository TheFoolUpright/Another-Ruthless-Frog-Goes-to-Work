using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

public class PigWorld : MonoBehaviour
{
    private PigRuntime pig;
    private NavMeshAgent agent;
    private StateMachine stateMachine;

    [SerializeField] private MissionInfo debugMission; // assign in inspector for testing
    [SerializeField] private PigsScriptableObject debugPigData;


    void Awake()
    {
        if (debugPigData == null)
        {
            //Debug.LogError("PigWorld is missing debugPigData", this);
            return;
        }

        PigRuntime testPig = new PigRuntime(debugPigData);
        Initialize(testPig);
    }

    public void Initialize(PigRuntime pigRuntime)
    {
        pig = pigRuntime;

        if (debugMission != null)
        {
            pig.currentMission = debugMission;
        }

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