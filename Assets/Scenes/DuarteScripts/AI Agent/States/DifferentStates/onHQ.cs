using System;
using UnityEngine;

public class onHQ : BaseState
{
    private PigRuntime _pig;

    public onHQ(PigRuntime pig) : base(null)
    {
        this._pig = pig;
    }

    public override Type Tick()
    {
        if (_pig.HasMission())
        {
            return typeof(onTravel);
        }

        return typeof(onHQ);
    }
}
