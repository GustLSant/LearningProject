using System;
using UnityEngine;
using Unity.Cinemachine;


public class TpsController : PlayerCameraController
{
    CinemachineThirdPersonFollow cinemachineThirdPersonFollow;
    float currentCameraSide = 1.0f;


    protected override void Awake()
    {
        base.Awake();
        cinemachineThirdPersonFollow = cinemachineCamera.GetComponent<CinemachineThirdPersonFollow>();
    }


    void Update()
    {
        if (pInpM.toggleTpsCameraSideAction.WasPressedThisFrame()) { toggleTpsCameraSide(); }
    }


    protected override void LateUpdate()
    {
        base.LateUpdate();
        cinemachineThirdPersonFollow.CameraSide = Mathf.Lerp(cinemachineThirdPersonFollow.CameraSide, currentCameraSide, 10 * Time.deltaTime);
    }


    override protected void handleBodyRotation()
    {
        Vector3 moveDirection = playerMovementController.moveDirection;
        bool isPlayerMoving = moveDirection != Vector3.zero;

        if (isPlayerMoving)
        {
            GameObject refBodyRot = new GameObject("RefRotBody");
            refBodyRot.transform.position = body.transform.position;
            Vector3 targetPos = body.transform.position + moveDirection * 100.0f;
            refBodyRot.transform.LookAt(targetPos);

            body.transform.rotation = Quaternion.Slerp(
                body.transform.rotation,
                refBodyRot.transform.rotation,
                15.0f * Time.deltaTime
            );

            Destroy(refBodyRot);
        }
        if (playerCombatManager.isAiming)
        {
            body.transform.eulerAngles = cameraRotation;
            body.transform.eulerAngles = new Vector3(0.0f, body.transform.eulerAngles.y, body.transform.eulerAngles.z);
        }
    }


    public void toggleTpsCameraSide()
    {
        if (currentCameraSide == 1.0f) { currentCameraSide = 0f; }
        else { currentCameraSide = 1.0f; }
    }
}
