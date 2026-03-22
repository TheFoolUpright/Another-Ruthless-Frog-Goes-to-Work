using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class onTravel : BaseState
{
    private NavMeshAgent _agent;
    private PigRuntime _pig;
    private bool _hasStarted; // New flag to prevent instant finishing

    public onTravel(PigRuntime pig, NavMeshAgent agent) : base(agent.gameObject)
    {
        this._pig = pig;
        _agent = agent;
    }

    public override void OnEnter(BaseState oldState)
    {
        _agent.enabled = true;
        _agent.isStopped = false;                                       //Add this to force the agent to continue
        _agent.SetDestination(_pig.currentMission.GetTarget().position);
    }

    public override Type Tick()
    {

        if (!_pig.HasMission())
        {
            return typeof(onHQ);
        }

        _agent.SetDestination(_pig.currentMission.GetTarget().position);


        if (_agent.pathPending)
        {
            return typeof(onTravel);                                    //If still calculating, just stay in this state and wait.
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance + 0.1f) //Check if we have arrived based on distance using a small buffer
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude < 0.01f) //Only transition if we are actually at the destination
            {
                return typeof(onMission);
            }
        }

        return typeof(onTravel);
    }
}