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

    private Transform hqTarget;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = gameObject.AddComponent<StateMachine>();

        if (pigSpriteRenderer == null)
        {
            pigSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        agent.updateRotation = false;
        agent.updateUpAxis = false;
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

            Initialize(testPig, null);
            GoToMission();
        }
    }

    public void Initialize(PigRuntime pigRuntime, Transform hqTransform)
    {
        pig = pigRuntime;
        hqTarget = hqTransform;
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
            { typeof(onMission), new onMission(pig, agent) },
            { typeof(onReturnToHQ), new onReturnToHQ(this, pig, agent, hqTarget) }
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

    public void DespawnAtHQ()
    {
        if (pig != null)
        {
            MissionInfo mission = pig.currentMission;

            pig.isDispatched = false;

            if (pig.homeInventorySlot != null && pig.homeInventorySlot.currentPig != null)
            {
                PigUI pigUI = pig.homeInventorySlot.currentPig.GetComponent<PigUI>();
                if (pigUI != null)
                {
                    pigUI.RefreshVisualState();
                }
            }

            if (mission != null)
            {
                mission.NotifyPigReturned(pig);
            }

            pig.currentMission = null;
        }

        Destroy(gameObject);
    }

    public Transform GetHQTarget()
    {
        return hqTarget;
    }

    public PigRuntime GetPig()
    {
        return pig;
    }
}