using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Memento pattern to store inputs
public class InputMemento
{
    public List<string> Inputs { get; private set; }

    public InputMemento(IEnumerable<string> inputs)
    {
        Inputs = new List<string>(inputs);
    }
}