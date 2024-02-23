using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public static event Action OpenInventoryEvent;
    public Vector2 MouseInput { get; private set; }
    private Vector2 movementInput;
    public float XInput { get; private set; }
    public float YInput { get; private set; }


    public bool ShiftInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool CroundInput { get; private set; }

    private InputHandler inputActions;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        inputActions = new InputHandler();
    }
    private void OnEnable()
    {

        inputActions.Enable();
        if (inputActions != null)
        {
            inputActions.Player.Movement.started += HandlerMovementInput;
            inputActions.Player.Movement.performed += HandlerMovementInput;
            inputActions.Player.Movement.canceled += HandlerMovementInput;
            inputActions.Mouse.Mouse.performed += i => MouseInput = i.ReadValue<Vector2>();

            inputActions.Player.Jumping.started += HandlerJumpInput;
            inputActions.Player.Jumping.canceled += HandlerJumpInput;

            inputActions.Player.Cround.started += HandlerCroundInput;
            inputActions.Player.Cround.canceled += HandlerCroundInput;

            inputActions.Player.Running.started += handlerShiftInput;
            inputActions.Player.Running.canceled += handlerShiftInput;

            inputActions.Player.Inventory.started += HandlerInventoryInput;

        }
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    public void HandlerMovementInput(InputAction.CallbackContext context)
    {
        //read value from context
        movementInput = context.ReadValue<Vector2>();

        //alway get value to 1
        XInput = (Vector2.right * movementInput).normalized.x;
        YInput = (Vector2.up * movementInput).normalized.y;

        //Debug.Log("Movement input");
    }

    public void HandlerJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
        }

        if (context.canceled)
        {
            JumpInput = false;
        }
    }

    public void handlerShiftInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShiftInput = true;
        }

        if (context.canceled)
        {
            ShiftInput = false;
        }
    }

    public void HandlerCroundInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            CroundInput = true;
        }

        if (context.canceled)
        {
            CroundInput = false;
        }
    }

    public void HandlerInventoryInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OpenInventoryEvent?.Invoke();
        }
    }

    public void ResetJumpInput()
    {
        JumpInput = false;
    }


}
