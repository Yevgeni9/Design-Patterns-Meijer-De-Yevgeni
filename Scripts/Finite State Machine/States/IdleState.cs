using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// States are made generic so that they can be implemented for different fighters
public class IdleState : IState
{
    private System.Action onEnter;
    private System.Action onUpdate;
    private System.Action onExit;

    public IdleState(System.Action onEnter = null, System.Action onUpdate = null, System.Action onExit = null)
    {
        this.onEnter = onEnter;
        this.onExit = onExit;
        this.onUpdate = onUpdate;
    }

    public void OnEnter()
    {
        onEnter?.Invoke();
    }

    public void OnUpdate()
    {
        onUpdate?.Invoke();
    }

    public void OnExit()
    {
        onExit?.Invoke();
    }
}
