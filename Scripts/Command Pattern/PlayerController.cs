using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Dictionary<KeyCode, ICommand> commandBindings; // Dictionary makes it easier to add new inputs
    private bool isAttacking = false;

    void Start()
    {
        commandBindings = new Dictionary<KeyCode, ICommand>()
        {
            { KeyCode.A, new PunchAttack(this, 0.3f) },
            { KeyCode.S, new KickAttack(this, 0.5f) },
            { KeyCode.W, new SlashAttack(this, 0.7f) },
            { KeyCode.D, new HeavySlashAttack(this, 1f) }
        };
    }

    void Update()
    {
        if (!isAttacking)
        {
            foreach (var binding in commandBindings)
            {
                if (Input.GetKeyDown(binding.Key))
                {
                    StartCoroutine(PerformCommand(binding.Value));
                    break;
                }
            }
        }
    }

    private IEnumerator PerformCommand(ICommand command)
    {
        isAttacking = true;
        command.Execute();

        float duration = 1f;
        if (command is AttackCommand attackCommand)
        {
            duration = attackCommand.duration;
        }

        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }
}
