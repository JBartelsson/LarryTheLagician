using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameInput : MonoBehaviour
{

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;
    public event EventHandler OnPause;
    private PlayerInput playerInputActions;

    public enum Binding
    {
        Move_Up, Move_Down, Move_Left, Move_Right, Interact, InteractAlternate, Pause
    }

    public static GameInput Instance { get; private set; }

    public event EventHandler OnUp;
    public event EventHandler OnRight;
    public event EventHandler OnLeft;
    public event EventHandler OnDown;
    public event EventHandler OnInteract1;
    public event EventHandler OnInteract2;
    public event EventHandler OnInteract3;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInput();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Up.performed += Up_performed;
        playerInputActions.Player.Left.performed += Left_performed;
        playerInputActions.Player.Right.performed += Right_performed;
        playerInputActions.Player.Down.performed += Down_performed;
        playerInputActions.Player.Interact1.performed += Interact1_performed;
        playerInputActions.Player.Interact2.performed += Interact2_performed;
        playerInputActions.Player.Interact3.performed += Interact3_performed;



    }

    private void Interact3_performed(InputAction.CallbackContext obj)
    {
        OnInteract3?.Invoke(null, EventArgs.Empty);
    }

    private void Interact2_performed(InputAction.CallbackContext obj)
    {
        OnInteract2?.Invoke(null, EventArgs.Empty);

    }

    private void Interact1_performed(InputAction.CallbackContext obj)
    {
        OnInteract1?.Invoke(null, EventArgs.Empty);

    }

    private void Down_performed(InputAction.CallbackContext obj)
    {
        OnDown?.Invoke(null, EventArgs.Empty);

    }

    private void Right_performed(InputAction.CallbackContext obj)
    {
        OnRight?.Invoke(null, EventArgs.Empty);

    }

    private void Left_performed(InputAction.CallbackContext obj)
    {
        OnLeft?.Invoke(null, EventArgs.Empty);

    }

    private void Up_performed(InputAction.CallbackContext obj)
    {
        OnUp?.Invoke(null, EventArgs.Empty);

    }

    private void OnDestroy()
    {
        playerInputActions.Dispose();
    }

}