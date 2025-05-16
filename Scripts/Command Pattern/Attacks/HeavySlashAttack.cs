using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavySlashAttack : AttackCommand
{
    public HeavySlashAttack(MonoBehaviour executor, float duration) : base(executor, duration) { }

    protected override IEnumerator PerformAttack()
    {
        Debug.Log("Start Heavy Slash");
        // play animation
        // play sfx
        yield return new WaitForSeconds(duration);
        Debug.Log("End Heavy Slash");
    }
}
