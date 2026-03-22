using System;
using UnityEngine;
using UnityEngine.AI;

public class onMission : BaseState
{
    private NavMeshAgent _agent;
    private float _timer;
    private PigRuntime _pig;

    public onMission(PigRuntime pig, NavMeshAgent agent) : base(agent.gameObject)
    {
        _pig = pig;
        _agent = agent;
    }

    public override void OnEnter(BaseState oldState)
    {
        _timer = 0f;

        if (_pig != null && _pig.currentMission != null)
        {
            _pig.currentMission.missionStartedAtLocation = true;
        }

        _agent.enabled = false;
        _agent.velocity = Vector3.zero;
    }

    public override void OnExit(BaseState newState)
    {
        _agent.enabled = true;
    }

    public override Type Tick()
    {
        if (_pig == null || _pig.currentMission == null)
        {
            return typeof(onReturnToHQ);
        }

        _timer += Time.deltaTime;

        // Use mission duration from MissionInfo if available
        if (_timer >= _pig.currentMission.missionDuration)
        {
            _pig.currentMission.missionOver = true;
            return typeof(onReturnToHQ);
        }

        return typeof(onMission);
    }
}