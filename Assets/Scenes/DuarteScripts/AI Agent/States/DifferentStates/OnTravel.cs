using System;
using UnityEngine;
using UnityEngine.AI;

public class onTravel : BaseState
{
    private NavMeshAgent _agent;
    private Transform _target;
    private Transform _HQ;
    
    public onTravel(GameObject gameObject, NavMeshAgent agent, Transform target) : base(gameObject)
    {
        _agent = agent;
        _target = target;
    }

    public override void OnEnter(BaseState oldState)
    {
        _agent.enabled = true;
    }

    public override Type Tick()
    {
        if (_agent != null)
        {
            if(_agent.pathPending && _agent.remainingDistance < 0.5f)
            {
                return typeof(onMission);
            }
        }
        return typeof(onTravel);
    }




    //Make the thing invisible
    //Make the thing stop
    //then
    //Make thing visible
    //Make thing go back to HQ
}
