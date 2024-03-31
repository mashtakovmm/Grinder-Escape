using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
public class InputReader : ScriptableObject, PlayerInput.IPlayerActions
{
    private PlayerInput _inputActions;

    public event Action<Vector2> MoveEvent;
    public event Action JumpEvent;
    public event Action SwitchLeftEvent;
    public event Action SwitchRightEvent;

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerInput();
            _inputActions.Player.SetCallbacks(this);
            SwitchMap(_inputActions.Player);
        }
    }

    private void SwitchMap(InputActionMap map)
    {
        _inputActions.Disable();
        map.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpEvent?.Invoke();
        }
    }

    public void OnSwitchLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SwitchLeftEvent?.Invoke();
        }
    }

    public void OnSwitchRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SwitchRightEvent?.Invoke();
        }
    }
}
