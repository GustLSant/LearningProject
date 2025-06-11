using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;


public class PlayerInputManager : MonoBehaviour
{
    [HideInInspector] public Vector2 moveInput = Vector2.zero;
    [HideInInspector] public bool sprintInput = false;
    [HideInInspector] public Vector2 lookRotInput = Vector2.zero;
    [HideInInspector] public bool aimingInput = false;

    public UnityEvent OnPlayerJump;
    public UnityEvent OnPlayerChangeCameraType;


    void OnMove(InputValue _value)
    {
        moveInput = _value.Get<Vector2>();
    }


    void OnSprint(InputValue _value)
    {
        sprintInput = Convert.ToBoolean(_value.Get<float>());
    }


    void OnLook(InputValue _value)
    {
        lookRotInput = _value.Get<Vector2>();
    }


    void OnJump()
    {
        OnPlayerJump?.Invoke();
    }


    void OnChangeCameraType()
    {
        OnPlayerChangeCameraType?.Invoke();
    }

    void OnAim(InputValue _value)
    {
        aimingInput = _value.Get<float>() == 1.0f;
    }
}
