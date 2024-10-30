using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        playerInput = this.GetComponent<PlayerInput>();

        playerInput.actions["MoveJoystick"].performed += OnMoveJoystick;
        playerInput.actions["MoveJoystick"].canceled += OnMoveJoystickCanceled;
    }

    private void OnMoveJoystick(InputAction.CallbackContext ctx)
    {
        inputVector = ctx.ReadValue<Vector2>().normalized;
    }

    private void OnMoveJoystickCanceled(InputAction.CallbackContext ctx)
    {
        inputVector = Vector2.zero;
    }

    public Vector2 GetInputPlayer()
    {
        return inputVector;
    }
}
