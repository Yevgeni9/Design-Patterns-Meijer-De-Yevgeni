using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// focused on adhering to open closed principle
public class Combo
{
    // get combo details
    public string Name { get; }
    public List<string> Sequence { get; }
    public Action OnComboMatched { get; }

    public Combo(string name, IEnumerable<string> sequence, Action onComboMatched)
    {
        Name = name;
        Sequence = new List<string>(sequence);
        OnComboMatched = onComboMatched;
    }

    // checks if combo matches
    public bool Matches(IReadOnlyList<string> buffer)
    {
        if (buffer.Count != Sequence.Count) return false;

        for (int i = 0; i < Sequence.Count; i++)
        {
            if (buffer[i] != Sequence[i])
                return false;
        }

        return true;
    }
}
