using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Dictionary<KeyCode, ICommand> commandBindings; // Used a dictionary to add new inputs
    private Queue<string> inputBuffer = new Queue<string>();
    [SerializeField] private float comboTimeWindow;
    private float lastInputTime;
    private bool isAttacking = false;

    private ComboInterpreter comboInterpreter = new ComboInterpreter();

    void Start()
    {
        SetupAttacks();
        SetupCombos();
    }

    void Update()
    {
        if (!isAttacking) // Only a single attack may happen at a time
        {
            foreach (var binding in commandBindings)
            {
                if (Input.GetKeyDown(binding.Key))
                {
                    string keyStr = GetInputKeyString();
                    AddToInputBuffer(keyStr);
                    StartCoroutine(PerformCommand(binding.Value));
                    break;
                }
            }
        }
    }

    private void SetupAttacks()
    {
        commandBindings = new Dictionary<KeyCode, ICommand>()
        {
            { KeyCode.A, new PunchAttack(this, 0.3f) },
            { KeyCode.S, new KickAttack(this, 0.5f) },
            { KeyCode.W, new SlashAttack(this, 0.7f) },
            { KeyCode.D, new HeavySlashAttack(this, 1f) }
        };
    }

    private void SetupCombos()
    {
        comboInterpreter.AddCombo(new Combo(
            "CircleCombo",
            new[] { "W", "A", "S", "D" },
            () => Debug.Log("Half Circle Combo")
        ));

        comboInterpreter.AddCombo(new Combo(
            "WWAA",
            new[] { "W", "A" },
            () => Debug.Log("Quarter Combo")
        ));
    }

    private IEnumerator PerformCommand(ICommand command)
    {
        isAttacking = true;
        command.Execute();

        float duration = 1f;
        if (command is AttackCommand attackCommand)
        {
            duration = attackCommand.Duration;
        }

        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }

    
    private string GetInputKeyString()
    {
        // for corner inputs
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) return "WA";
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) return "AS";
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) return "SD";
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) return "DW";

        if (Input.GetKeyDown(KeyCode.W)) return "W";
        if (Input.GetKeyDown(KeyCode.A)) return "A";
        if (Input.GetKeyDown(KeyCode.S)) return "S";
        if (Input.GetKeyDown(KeyCode.D)) return "D";

        return "";
    }

    private void AddToInputBuffer(string keyStr)
    {
        if (string.IsNullOrEmpty(keyStr)) return;

        float currentTime = Time.time;
        if (currentTime - lastInputTime > comboTimeWindow)
        {
            inputBuffer.Clear();
        }

        lastInputTime = currentTime;
        inputBuffer.Enqueue(keyStr);

        while (inputBuffer.Count > 8)
            inputBuffer.Dequeue();

        var snapshot = SaveInputState();
        comboInterpreter.Interpret(snapshot.Inputs);
    }

    private InputMemento SaveInputState()
    {
        return new InputMemento(inputBuffer);
    }
}
