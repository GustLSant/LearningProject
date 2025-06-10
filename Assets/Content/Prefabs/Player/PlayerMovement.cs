using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerMovement : MonoBehaviour
{
    PlayerCameraManager playerCameraManager;
    CharacterController controller;

    [SerializeField] const float MAX_WALKING_SPEED = 6.0f;
    [SerializeField] const float SPRINT_SPEED_MULTIPLIER = 2.0f;
    [SerializeField] const float MOVE_SPEED_ACCELERATION = 12.0f;
    private float currentMoveSpeed = 0.0f;
    private bool isSprinting = false;
    [HideInInspector] public Vector3 moveDirection;
    Vector2 moveInput;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCameraManager = GetComponent<PlayerCameraManager>();
    }


    void FixedUpdate()
    {
        handleMovement();
    }


    void handleMovement()
    {
        GameObject pivotRot = playerCameraManager.currentPivotRot;
        
        moveDirection = pivotRot.transform.forward * moveInput.y + pivotRot.transform.right * moveInput.x;
        moveDirection.y = 0.0f;
        moveDirection = moveDirection.normalized;

        int isPlayerMovingInt = Convert.ToInt32(moveDirection != Vector3.zero);
        int isPlayerStandingStillInt = Convert.ToInt32(moveDirection == Vector3.zero);

        float currentSprintSpeedMultiplier = (isSprinting && isPlayerMovingInt==1) ? SPRINT_SPEED_MULTIPLIER : 1.0f;

        currentMoveSpeed = Mathf.Lerp(
            currentMoveSpeed,
            isPlayerStandingStillInt * 0.0f + isPlayerMovingInt * (MAX_WALKING_SPEED * currentSprintSpeedMultiplier),
            MOVE_SPEED_ACCELERATION * Time.deltaTime
        );

        controller.Move(moveDirection * currentMoveSpeed * Time.deltaTime);
    }


    void OnMove(InputValue _value)
    {
        moveInput = _value.Get<Vector2>();
    }

    void OnSprint(InputValue _value)
    {
        isSprinting = Convert.ToBoolean(_value.Get<float>());
    }
}
