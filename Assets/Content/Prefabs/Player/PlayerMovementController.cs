using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class PlayerMovementController : MonoBehaviour
{
    PlayerInputManager pInpM; // playerInputManager

    PlayerCameraManager playerCameraManager;
    PlayerJumpController playerJumpManager;
    PlayerCombatController playerCombatController;
    CharacterController controller;

    [SerializeField] const float MAX_WALKING_SPEED = 4.0f;
    [SerializeField] const float MOVE_SPEED_ACCELERATION = 12.0f;
    [HideInInspector] public float currentMoveSpeed = 0.0f;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public Vector2 moveInput;

    [SerializeField] const float SPRINT_SPEED_MULTIPLIER = 2.0f;
    [HideInInspector] public bool isSprinting = false;


    void Awake()
    {
        pInpM = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();

        controller = GetComponent<CharacterController>();
        playerCameraManager = GetComponent<PlayerCameraManager>();
        playerJumpManager = GetComponent<PlayerJumpController>();
        playerCombatController = GetComponent<PlayerCombatController>();
    }


    void Update()
    {
        getInputValues();
    }


    void FixedUpdate()
    {
        handleMovement();
    }


    void getInputValues()
    {
        if (S_GameSettings.instance.isSprintHoldMode) { isSprinting = pInpM.sprintAction.IsPressed(); }
        else
        {
            if (pInpM.sprintAction.WasPressedThisFrame()) { isSprinting = !isSprinting; }
        }

        moveInput = pInpM.moveAction.ReadValue<Vector2>();
    }


    void handleMovement()
    {
        GameObject pivotRot = playerCameraManager.currentPivotRot;

        moveDirection = pivotRot.transform.forward * moveInput.y + pivotRot.transform.right * moveInput.x;
        moveDirection.y = 0.0f;
        moveDirection = moveDirection.normalized;

        int isPlayerMovingInt = Convert.ToInt32(moveDirection != Vector3.zero);
        int isPlayerStandingStillInt = Convert.ToInt32(moveDirection == Vector3.zero);

        // so corre se nao tiver mirando ou se movimentando
        isSprinting = isSprinting && !playerCombatController.isAiming && (moveDirection != Vector3.zero);
        // em primeira pessoa, so corre se estiver andando pra frente
        if (playerCameraManager.currentCameraMode == PlayerCameraManager.CameraType.FPS) { isSprinting = isSprinting && (moveInput.y > 0.0f); }
        float currentSprintSpeedMultiplier = (isSprinting && isPlayerMovingInt == 1) ? SPRINT_SPEED_MULTIPLIER : 1.0f;

        float aimingSpeedPenaulty = 1.0f - Convert.ToInt32(playerCombatController.isAiming) * 0.5f;
        currentMoveSpeed = Mathf.Lerp(
            currentMoveSpeed,
            isPlayerStandingStillInt * 0.0f + isPlayerMovingInt * (MAX_WALKING_SPEED * currentSprintSpeedMultiplier * aimingSpeedPenaulty),
            MOVE_SPEED_ACCELERATION * Time.deltaTime
        );

        Vector3 verticalMovement = new Vector3(0.0f, playerJumpManager.currentVerticalMovement, 0.0f);

        controller.Move((moveDirection * currentMoveSpeed * aimingSpeedPenaulty + verticalMovement) * Time.deltaTime);
        // CollisionFlags flags = 
        // if ((flags & CollisionFlags.Sides) != 0)
        // {
        //     Debug.Log("Colidiu com algo na lateral.");
        // }
        // if ((flags & CollisionFlags.Above) != 0)
        // {
        //     Debug.Log("Colidiu com algo acima.");
        // }
        // if ((flags & CollisionFlags.Below) != 0)
        // {
        //     Debug.Log("Colidiu com algo abaixo.");
        // }
    }
    
    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     Debug.Log("Colidiu com: " + hit.gameObject.name);
    // }
}
