using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Interpreter pattern
public class ComboInterpreter
{
    private List<Combo> combos = new List<Combo>(); // stores combos

    public void AddCombo(Combo combo)
    {
        combos.Add(combo);
    }

    public void Interpret(IReadOnlyList<string> inputBuffer)
    {
        foreach (var combo in combos)
        {
            if (combo.Matches(inputBuffer))
            {
                Debug.Log($"Combo Matched: {combo.Name}");
                combo.OnComboMatched?.Invoke();
                break;
            }
        }
    }
}
