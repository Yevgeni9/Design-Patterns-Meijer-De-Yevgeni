using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackCommand : ICommand
{
    protected MonoBehaviour executor; // For using Coroutines
    public readonly float duration;

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
