using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour
{
    public InputActionAsset inputActions;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction sprintAction;
    public InputAction jumpAction;
    public InputAction aimAction;
    public InputAction changeCameraModeAction;
    public InputAction toggleTpsCameraSideAction;


    void OnEnable() { inputActions.FindActionMap("Player").Enable(); }

    void OnDisable() { inputActions.FindActionMap("Player").Disable(); }

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        jumpAction = InputSystem.actions.FindAction("Jump");
        aimAction = InputSystem.actions.FindAction("Aim");
        changeCameraModeAction = InputSystem.actions.FindAction("ChangeCameraMode");
        toggleTpsCameraSideAction = InputSystem.actions.FindAction("ToggleTpsCameraSide");
    }
}
