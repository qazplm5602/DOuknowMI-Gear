using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions, IUIActions
{
    #region Player
    public event Action JumpEvent;
    public event Action DashEvent;
    #endregion
    #region UI
    public event Action StatOpenEvent;
    public event Action PauseEvent;
    public event Action<float> PauseWheelEvent;
    #endregion

    private Controls _controls;
    public Controls GetControl()
    {
        return _controls;
    }

    public Vector2 _xMovement { get; private set; }
    public Vector2 mousePosition { get; private set; }

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);
            _controls.UI.SetCallbacks(this);
        }

        _controls.Player.Enable();
        _controls.UI.Enable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _xMovement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) {
            JumpEvent?.Invoke();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed) {
            DashEvent?.Invoke();
        }
    }

    public void OnStat(InputAction.CallbackContext context)
    {
        if (context.performed) {
            StatOpenEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) {
            PauseEvent?.Invoke();
        }
    }

    public void OnPauseWheel(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        if (context.performed) {
            PauseWheelEvent?.Invoke(value);
        }
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}