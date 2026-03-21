using System;
using UnityEngine;

public class onHQ : BaseState
{
    private float _timer;
    private Transform _mission;

    public onHQ(GameObject gameobject, Transform target) : base(gameobject)
    {
        _mission = target;
    }

    public override Type Tick()
    {
        if (_mission != null)
        {
            return typeof(onTravel);
        }
        return typeof(onHQ);
    }
}
