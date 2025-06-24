using System;
using UnityEngine;


[DisallowMultipleComponent]
public class TpsCameraController : PlayerCameraController
{
    GameObject refBodyRot;
    float currentCameraSide = 1.0f;
    readonly Vector3 DEFAULT_CAMERA_POS = new Vector3(1.0f, 0.5f, -2.75f);


    protected override void Awake()
    {
        base.Awake();
        refBodyRot = transform.Find("PivotRefRot").gameObject;
    }


    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (pInpM.toggleTpsCameraSideAction.WasPressedThisFrame()) { toggleTpsCameraSide(); }
        handleCameraPosition();
        handleCameraDelayEffect();
    }


    void handleCameraDelayEffect()
    {
        Vector3 targetOffset = Vector3.zero;

        targetOffset.x -= playerMovementController.moveInput.x;
        targetOffset.z -= playerMovementController.moveInput.y;
        targetOffset = targetOffset.normalized;

        // calculo de intensidade basedo na quantidade de movimento
        targetOffset *= 0.3f + (Convert.ToInt32(playerMovementController.isSprinting) * 0.3f);
        targetOffset *= 1.0f - (Convert.ToInt32(playerCombatController.isAiming) * 0.5f);

        pivotRecoil.transform.localPosition = Vector3.Lerp(
            pivotRecoil.transform.localPosition,
            targetOffset,
            Time.deltaTime * 7.0f
        );
    }


    void handleCameraPosition()
    {
        Vector3 targetPosition = DEFAULT_CAMERA_POS;
        targetPosition.x *= currentCameraSide;

        cameraObject.transform.localPosition = targetPosition;
    }


    override protected void handleBodyRotation()
    {
        Vector3 moveDirection = playerMovementController.moveDirection;
        bool isPlayerMoving = moveDirection != Vector3.zero;

        if (isPlayerMoving)
        {
            Vector3 targetPos = body.transform.position + moveDirection * 100.0f;
            refBodyRot.transform.LookAt(targetPos);

            body.transform.rotation = Quaternion.Slerp(
                body.transform.rotation,
                refBodyRot.transform.rotation,
                15.0f * Time.deltaTime
            );
        }
        if (playerCombatController.isAiming)
        {
            Quaternion targetRotation = Quaternion.Euler(0.0f, cameraRotation.y, 0.0f);
            body.transform.rotation = targetRotation;
        }
    }


    public void toggleTpsCameraSide()
    {
        currentCameraSide *= 1.0f;
    }
}
