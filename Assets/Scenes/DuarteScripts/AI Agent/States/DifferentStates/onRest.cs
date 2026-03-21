using System;
using UnityEngine;

public class onRest : BaseState
{
    public onRest(GameObject gameObject) : base(gameObject)
    {

    }

    public override Type Tick()
    {
        //if (gameObject.stamina >= 3 &&)
        //{
        //    return typeof(onHQ);
        //}
        
        return typeof(onRest);
    }
}
