using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Health;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerStateController stateController;

    private void Start()
    {
        stateController.Initialize(inputHandler);
    }
}
