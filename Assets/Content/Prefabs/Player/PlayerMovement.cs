using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerMovement : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    PlayerCameraManager playerCameraManager;
    PlayerJumpManager playerJumpManager;
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
        playerInputManager = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();
        controller = GetComponent<CharacterController>();
        playerCameraManager = GetComponent<PlayerCameraManager>();
        playerJumpManager = GetComponent<PlayerJumpManager>();
    }


    void FixedUpdate()
    {
        getInputValues();
        handleMovement();
    }


    void getInputValues()
    {
        moveInput = playerInputManager.moveInput;
        isSprinting = playerInputManager.sprintInput;
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

        Vector3 verticalMovement = new Vector3(0.0f, playerJumpManager.currentVerticalMovement, 0.0f);

        controller.Move((moveDirection * currentMoveSpeed + verticalMovement) * Time.deltaTime);
    }
}
