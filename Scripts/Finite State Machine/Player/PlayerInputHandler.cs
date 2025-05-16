using System;
using UnityEngine;

// Simple implementation of an InputHandler

[RequireComponent(typeof(Player))]
public class PlayerInputHandler : MonoBehaviour
{
    public bool PunchPressed { get; private set; }
    public bool KickPressed { get; private set; }
    public bool SlashPressed { get; private set; }
    public bool HeavySlashPressed { get; private set; }

    private void Update()
    {
        PunchPressed = Input.GetKeyDown(KeyCode.A);
        KickPressed = Input.GetKeyDown(KeyCode.S);
        SlashPressed = Input.GetKeyDown(KeyCode.W);
        HeavySlashPressed = Input.GetKeyDown(KeyCode.D);
    }
}
