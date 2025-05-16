using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// States are made generic so that they can be implemented for different fighters
public class KickState : IState
{
    private float duration;
    private float timer;
    private System.Action onComplete;
    private System.Action onEnter;
    private System.Action onUpdate;
    private System.Action onExit;

    public KickState(float duration, System.Action onComplete, System.Action onEnter = null, System.Action onExit = null, System.Action onUpdate = null)
    {
        this.duration = duration;
        this.onComplete = onComplete;
        this.onEnter = onEnter;
        this.onExit = onExit;
        this.onUpdate = onUpdate;
    }

    public void OnEnter()
    {
        timer = 0f;
        onEnter?.Invoke();
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;
        onUpdate?.Invoke();

        if (timer >= duration)
        {
            onComplete?.Invoke();
        }
    }

    public void OnExit()
    {
        onExit?.Invoke();
    }
}
