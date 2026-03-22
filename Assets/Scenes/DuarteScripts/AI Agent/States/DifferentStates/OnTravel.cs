using System;
using UnityEngine;
using UnityEngine.AI;

public class onTravel : BaseState
{
    private NavMeshAgent _agent;
    private PigRuntime _pig;

    public onTravel(PigRuntime pig, NavMeshAgent agent) : base(agent.gameObject)
    {
        _pig = pig;
        _agent = agent;
    }

    public override void OnEnter(BaseState oldState)
    {
        if (_pig == null || _pig.currentMission == null)
            return;

        _agent.enabled = true;
        _agent.isStopped = false;
        _agent.SetDestination(_pig.currentMission.GetTarget().position);
    }

    public override Type Tick()
    {
        if (_pig == null || !_pig.HasMission())
        {
            return typeof(onHQ);
        }

        _agent.SetDestination(_pig.currentMission.GetTarget().position);

        if (_agent.pathPending)
        {
            return typeof(onTravel);
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance + 0.1f)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude < 0.01f)
            {
                return typeof(onMission);
            }
        }

        return typeof(onTravel);
    }
}