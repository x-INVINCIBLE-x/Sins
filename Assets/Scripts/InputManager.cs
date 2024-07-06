using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, PlayerControls.IPlayerActions
{
    private PlayerControls controls;
    public static InputManager instance;

    public Vector2 movement;

    public event Action JumpEvent;
    public event Action AttackEvent;
    public event Action BlockEvent;
    public event Action DashEvent;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        controls = new PlayerControls();
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent?.Invoke();
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (!context.started)
            BlockEvent?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
            DashEvent?.Invoke();
    }
}
