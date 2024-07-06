using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, PlayerControls.IPlayerActions
{
    public static InputManager instance;
    public Vector2 movement;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
