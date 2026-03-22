using System;
using UnityEngine;
using UnityEngine.AI;

public class onReturnToHQ : BaseState
{
    private PigWorld _pigWorld;
    private PigRuntime _pig;
    private NavMeshAgent _agent;
    private Transform _hqTarget;

    public onReturnToHQ(PigWorld pigWorld, PigRuntime pig, NavMeshAgent agent, Transform hqTarget)
        : base(agent.gameObject)
    {
        _pigWorld = pigWorld;
        _pig = pig;
        _agent = agent;
        _hqTarget = hqTarget;
    }

    public override void OnEnter(BaseState oldState)
    {
        if (_agent == null || _hqTarget == null)
            return;

        _agent.enabled = true;
        _agent.isStopped = false;
        _agent.SetDestination(_hqTarget.position);
    }

    public override Type Tick()
    {
        if (_hqTarget == null)
        {
            _pigWorld.DespawnAtHQ();
            return typeof(onHQ);
        }

        if (!_agent.enabled)
            return typeof(onReturnToHQ);

        _agent.SetDestination(_hqTarget.position);

        if (_agent.pathPending)
            return typeof(onReturnToHQ);

        if (_agent.remainingDistance <= _agent.stoppingDistance + 0.001f)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude < 0.01f)
            {
                _pigWorld.DespawnAtHQ();
                return typeof(onHQ);
            }
        }

        return typeof(onReturnToHQ);
    }
}