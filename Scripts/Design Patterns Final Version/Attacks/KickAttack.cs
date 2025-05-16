using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickAttack : AttackCommand
{
    public KickAttack(MonoBehaviour executor, float duration) : base(executor, duration) { }

    protected override IEnumerator PerformAttack()
    {
        Debug.Log("Start Kick");
        // play animation
        // play sfx
        yield return new WaitForSeconds(duration);
        Debug.Log("End Kick");
    }
}
