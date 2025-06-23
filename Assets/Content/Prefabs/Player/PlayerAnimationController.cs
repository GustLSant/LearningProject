using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator tpsAnimator;
    [SerializeField] GameObject tpsMeshObject;
    [SerializeField] PlayerMovementController movementController;
    [SerializeField] PlayerJumpController playerJumpController;
    [SerializeField] PlayerCombatController playerCombatController;
    PlayerCameraController playerCameraController;
    private string currentAnimation = "";


    void Awake()
    {
        tpsAnimator = tpsMeshObject.GetComponent<Animator>();
        playerCameraController = GameObject.FindGameObjectWithTag("PlayerTpsCameraController").GetComponent<PlayerCameraController>();
    }


    void LateUpdate()
    {
        handleAnimChanges();
        handleAnimParamsChanges();
    }


    void handleAnimChanges()
    {
        string targetAnimation = "";
        float animChangeSpeed = 0.2f;

        // transicoes das animacoes
        if (playerJumpController.isGrounded)
        {
            if (movementController.moveDirection == Vector3.zero) { targetAnimation = "Idle"; }
            else if (movementController.isSprinting) { targetAnimation = "Running"; }
            else{ targetAnimation = "Walking"; }
        }
        else { targetAnimation = "Falling"; animChangeSpeed = 0.15f; }

        if (playerCombatController.isAiming) { targetAnimation = "AimingBlendTree"; animChangeSpeed = 0.075f; }

        // tempos de retorno das animacoes
        if (currentAnimation == "AimingBlendTree") { animChangeSpeed = 0.125f; }
        else if (currentAnimation == "Falling") { animChangeSpeed = 0.15f; }


        changeAnimation(targetAnimation, animChangeSpeed);
    }


    void handleAnimParamsChanges()
    {
        float targetAimingAngle = (playerCameraController.cameraRotation.x / -120.0f) + 0.5f;
        tpsAnimator.SetFloat("AimingBlendDir", targetAimingAngle);
    }


    void changeAnimation(string _newAnim, float _animChangeSpeed=0.2f)
    {
        if (_newAnim != currentAnimation)
        {
            currentAnimation = _newAnim;
            if(tpsAnimator.isActiveAndEnabled){ tpsAnimator.CrossFade(_newAnim, _animChangeSpeed); }
        }
    }
}
