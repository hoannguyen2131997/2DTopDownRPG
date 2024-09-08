using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputSystemSingleton : MonoBehaviour
{
    private PlayerInputActions _actions;

    public event EventHandler OnDashAction;
    //public static GameInputSystemSingleton Instance { get; private set; }

    public event EventHandler<OnItemInventoryPressedEventArgs> OnItemInventory;

    public class OnItemInventoryPressedEventArgs : EventArgs
    {
        public int indexInventory;
    }

    public event EventHandler<OnAttackingPressedEventArgs> OnAttacking;
    public class OnAttackingPressedEventArgs : EventArgs
    {
        public bool PressAttacking;
    }

    private EventsPlayerManager eventsPlayerManager;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        //if (Instance != null && Instance != this)
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    Instance = this;
        //}

        eventsPlayerManager = GetComponentInParent<EventsPlayerManager>();
        _actions = new PlayerInputActions();
        _actions.Movement.Enable();
        _actions.Combat.Enable();
        _actions.Inventory.Enable();
    }

    private void Start()
    {
        _actions.Combat.AttackMobile.started += eventsPlayerManager.StartAttackingPlayer;
        _actions.Combat.AttackMobile.canceled += eventsPlayerManager.EndAttackingPlayer;

        _actions.Combat.Dash.performed += Dash_Performed;
        _actions.Inventory.Keyboard.performed += GetIndexInventory;
    }

    public void DisableMovementPlayer()
    {
        _actions.Movement.Disable();
    }

    //private void EndAttacking(InputAction.CallbackContext context)
    //{
    //    OnAttacking?.Invoke(this, new OnAttackingPressedEventArgs { PressAttacking = false });
    //}

    //private void StartAttacking(InputAction.CallbackContext context)
    //{
    //    OnAttacking?.Invoke(this, new OnAttackingPressedEventArgs { PressAttacking = true });
    //}

    private void GetIndexInventory(InputAction.CallbackContext context)
    {
        int index = (int)context.ReadValue<float>();
        OnItemInventory?.Invoke(this, new OnItemInventoryPressedEventArgs { indexInventory = index });
    }

    private void Dash_Performed(InputAction.CallbackContext context)
    {
        OnDashAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        //_actions.Combat.Attack.started -= StartAttacking;
        //_actions.Combat.Attack.canceled -= EndAttacking;

        //_actions.Combat.AttackMobile.started -= StartAttacking;
        //_actions.Combat.AttackMobile.canceled -= EndAttacking;

        _actions.Combat.AttackMobile.started -= eventsPlayerManager.StartAttackingPlayer;
        _actions.Combat.AttackMobile.canceled -= eventsPlayerManager.EndAttackingPlayer;

        _actions.Combat.Dash.performed -= Dash_Performed;
        _actions.Inventory.Keyboard.performed -= GetIndexInventory;
        _actions.Dispose();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _actions.Movement.Move.ReadValue<Vector2>();
        Vector2 inputVectorJoystick = _actions.Movement.MoveJoystick.ReadValue<Vector2>();
        if(inputVectorJoystick != Vector2.zero)
        {
            inputVector = inputVectorJoystick;
        }
       
        //Debug.Log("x:y " + inputVector.x + " : " + inputVector.y);
        return inputVector;
    }
}
