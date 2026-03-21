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
        this._pig = pig;
        _agent = agent;
    }

    public override void OnEnter(BaseState oldState)
    {
        _timer = 0f;
        _agent.enabled = false;
        _agent.velocity = Vector3.zero;
    }

    public override void OnExit(BaseState newState)
    {
        _agent.enabled = true;
    }

    public override Type Tick()
    {
        _timer += Time.deltaTime; // This line is missing in your current file!

        if (_timer >= 10f)
        {
            return typeof(onTravel);
        }

        return typeof(onMission);
    }
}
