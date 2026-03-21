using System;
using UnityEngine;

public abstract class BaseState
{
    protected GameObject gameObject;
    protected Transform transform;

    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    public abstract Type Tick();
    public virtual void OnEnter(BaseState oldState) { }
    public virtual void OnExit(BaseState newState) { }
}