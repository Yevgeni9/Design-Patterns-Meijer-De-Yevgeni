using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : AttackCommand
{
    public PunchAttack(MonoBehaviour executor, float duration) : base(executor, duration) { }

    protected override IEnumerator PerformAttack()
    {
        Debug.Log("Start Punch");
        // play animation
        // play sfx
        yield return new WaitForSeconds(duration);
        Debug.Log("End Punch");
    }
}
