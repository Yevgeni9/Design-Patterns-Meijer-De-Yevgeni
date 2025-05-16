using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCommand : ICommand
{
    protected MonoBehaviour executor; // for using Coroutines
    protected float duration;
    public float Duration => duration;

    public AttackCommand(MonoBehaviour executor, float duration)
    {
        this.executor = executor;
        this.duration = duration;
    }

    public void Execute()
    {
        executor.StartCoroutine(PerformAttack());
    }

    protected abstract IEnumerator PerformAttack();
}
