using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

public class PigWorld : MonoBehaviour
{
    private PigRuntime pig;
    private NavMeshAgent agent;
    private StateMachine stateMachine;
    private bool isInitialized;

    [SerializeField] private SpriteRenderer pigSpriteRenderer;

    [SerializeField] private MissionInfo debugMission;
    [SerializeField] private PigsScriptableObject debugPigData;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = gameObject.AddComponent<StateMachine>();

        if (pigSpriteRenderer == null)
        {
            pigSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Start()
    {
        if (!isInitialized && debugPigData != null)
        {
            PigRuntime testPig = new PigRuntime(debugPigData);

            if (debugMission != null)
            {
                testPig.currentMission = debugMission;
            }

            Initialize(testPig);
            GoToMission();
        }
    }

    public void Initialize(PigRuntime pigRuntime)
    {
        pig = pigRuntime;
        isInitialized = true;

        if (pigSpriteRenderer != null && pig != null && pig.data != null)
        {
            pigSpriteRenderer.sprite = pig.data.GetImage();
        }
        else
        {
            Debug.LogError("PigWorld could not assign sprite. Missing renderer or pig data.", this);
        }

        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(onHQ), new onHQ(pig) },
            { typeof(onTravel), new onTravel(pig, agent) },
            { typeof(onMission), new onMission(pig, agent) }
        };

        stateMachine.SetStates(states);
    }

    public void GoToMission()
    {
        if (pig == null || !pig.HasMission())
        {
            Debug.LogWarning("PigWorld cannot go to mission because pig or mission is missing.");
            return;
        }

        stateMachine.SwitchToNewState(typeof(onTravel));
    }

    public PigRuntime GetPig()
    {
        return pig;
    }
}