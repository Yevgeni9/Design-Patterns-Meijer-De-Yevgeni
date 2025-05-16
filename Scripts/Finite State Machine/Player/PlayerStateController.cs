using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

// This implementation of the PlayerStateController is quite concrete, although this has its downsides, it will be useful for being able to reuse the attack states on different characters
// This class can also easily be reworked into a fighter specific class
[RequireComponent(typeof(Player))]
public class PlayerStateController : MonoBehaviour
{
    private FiniteStateMachine fsm;
    private PlayerInputHandler input;

    public void Initialize(PlayerInputHandler inputHandler)
    {
        fsm = new FiniteStateMachine();
        input = inputHandler;

        IState idleState = new IdleState(
            onEnter: () => Debug.Log("Start idle animation"),
            onUpdate: () => Debug.Log("Is idling"),
            onExit: () => Debug.Log("End idle animation")
        );

        IState punchState = new PunchState(
            duration: 0.3f,
            onEnter: () => Debug.Log("Start Punch"),
            onUpdate: () => Debug.Log("Is in Punch"),
            onExit: () => Debug.Log("End Punch"),
            onComplete: () => fsm.SwitchState(idleState)
        );

        IState kickState = new KickState(
            duration: 0.5f,
            onEnter: () => Debug.Log("Start Kick"),
            onUpdate: () => Debug.Log("Is in Kick"),
            onExit: () => Debug.Log("End Kick"),
            onComplete: () => fsm.SwitchState(idleState)
        );

        IState slashState = new SlashState(
            duration: 0.7f,
            onEnter: () => Debug.Log("Start Slash"),
            onUpdate: () => Debug.Log("Is in Slash"),
            onExit: () => Debug.Log("End Slash"),
            onComplete: () => fsm.SwitchState(idleState)
        );

        IState heavySlashState = new HeavySlashState(
            duration: 1f,
            onEnter: () => Debug.Log("Start Slash"),
            onUpdate: () => Debug.Log("Is in Slash"),
            onExit: () => Debug.Log("End Slash"),
            onComplete: () => fsm.SwitchState(idleState)
        );

        fsm.AddState(idleState);
        fsm.AddState(punchState);
        fsm.AddState(kickState);
        fsm.AddState(slashState);
        fsm.AddState(heavySlashState);

        fsm.AddTransition(new Transition(idleState, punchState, () => input.PunchPressed));
        fsm.AddTransition(new Transition(idleState, kickState, () => input.KickPressed));
        fsm.AddTransition(new Transition(idleState, slashState, () => input.SlashPressed));
        fsm.AddTransition(new Transition(idleState, slashState, () => input.HeavySlashPressed));

        // Default state is idle
        fsm.SwitchState(idleState);
    }

    private void Update()
    {
        fsm.OnUpdate();
    }
}
