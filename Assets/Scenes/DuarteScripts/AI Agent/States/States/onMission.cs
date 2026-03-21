using System;
using UnityEngine;
using UnityEngine.AI;

public class onMission : BaseState
{
    private NavMeshAgent _agent;
    private float _timer;

    public onMission(GameObject gameObject, NavMeshAgent agent) : base(gameObject)
    {
        _agent = agent;
    }

    public override void OnEnter(BaseState oldState)
    {
        _timer = 0f;
    }
    
    public override Type Tick()
    {
        if (_timer >= 10f)
        {
            return typeof(onTravel);
        }

        return typeof(onMission);
    }
}
