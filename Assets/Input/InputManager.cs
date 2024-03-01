using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    #region Singleton 
    public static InputManager Instance;
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

    #endregion
    public static event Action OpenInventoryEvent;

    public Vector2 MouseInput { get; private set; }

    #region Movement input
    private Vector2 movementInput;
    public float XInput { get; private set; }
    public float YInput { get; private set; }
    #endregion

    public bool ShiftInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool CroundInput { get; private set; }
    public bool AttackInput { get; private set; }


    private void OnEnable()
    {

        inputActions.Enable();
        if (inputActions != null)
        {
            // movement input
            inputActions.Player.Movement.started += HandlerMovementInput;
            inputActions.Player.Movement.performed += HandlerMovementInput;
            inputActions.Player.Movement.canceled += HandlerMovementInput;
            // get mouse movement input
            inputActions.Mouse.Mouse.performed += i => MouseInput = i.ReadValue<Vector2>();

            // jumping input
            inputActions.Player.Jumping.started += HandlerJumpInput;
            inputActions.Player.Jumping.canceled += HandlerJumpInput;

            // cround input
            inputActions.Player.Cround.started += HandlerCroundInput;
            inputActions.Player.Cround.canceled += HandlerCroundInput;

            //Sprinting input
            inputActions.Player.Running.started += handlerShiftInput;
            inputActions.Player.Running.canceled += handlerShiftInput;

            //Attack input
            inputActions.Mouse.Attack.started += HandlerAttackInput;
            inputActions.Mouse.Attack.canceled += HandlerAttackInput;



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

    public void HandlerAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput = true;
        }

        if (context.canceled)
        {
            AttackInput = false;
        }
    }


}
