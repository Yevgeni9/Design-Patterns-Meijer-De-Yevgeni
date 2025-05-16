using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : AttackCommand
{
    public SlashAttack(MonoBehaviour executor, float duration) : base(executor, duration) { }

    protected override IEnumerator PerformAttack()
    {
        Debug.Log("Start Slash");
        // play animation
        // play sfx
        yield return new WaitForSeconds(duration);
        Debug.Log("End Slash");
    }
}
