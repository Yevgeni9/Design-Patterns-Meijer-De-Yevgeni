using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Uses Valentijns example of a Finite State Machine
public class FiniteStateMachine
{
    public IState currentState { get; private set; }
    private Dictionary<System.Type, IState> allStates = new Dictionary<System.Type, IState>();
    private List<Transition> transitions = new List<Transition>();
    private List<Transition> currentTransitions = new List<Transition>();

    public void OnUpdate()
    {
        foreach (var transition in currentTransitions)
        {
            if (transition.Evaluate())
            {
                SwitchState(transition.toState);
                return;
            }
        }

        currentState?.OnUpdate();
    }

    public void AddState(IState state)
    {
        allStates.TryAdd(state.GetType(), state);
    }

    public void RemoveState(System.Type type)
    {
        if (allStates.ContainsKey(type))
        {
            allStates.Remove(type);
        }
    }

    public void SwitchState(IState state)
    {
        currentState?.OnExit();
        currentState = state;
        if (currentState != null)
        {
            currentTransitions = transitions.FindAll(x => x.fromState == currentState || x.fromState == null);
        }
        currentState?.OnEnter();
    }

    public void SwitchState(System.Type type)
    {
        if (allStates.ContainsKey(type))
        {
            SwitchState(allStates[type]);
        }
    }

    public void AddTransition(Transition transition)
    {
        transitions.Add(transition);
    }
}

public class Transition
{
    public readonly IState fromState;
    public readonly IState toState;
    private System.Func<bool> condition;

    public Transition(IState fromState, IState toState, System.Func<bool> condition)
    {
        this.fromState = fromState;
        this.toState = toState;
        this.condition = condition;
    }

    public bool Evaluate()
    {
        return condition();
    }
}

public interface IState
{
    void OnUpdate();
    void OnEnter();
    void OnExit();
}