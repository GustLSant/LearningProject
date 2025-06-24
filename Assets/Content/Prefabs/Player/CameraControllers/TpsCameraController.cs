using System;
using UnityEngine;


[DisallowMultipleComponent]
public class TpsCameraController : PlayerCameraController
{
    //     CinemachineThirdPersonFollow cinemachineThirdPersonFollow;
    //     float currentCameraSide = 1.0f;


    //     protected override void Awake()
    //     {
    //         base.Awake();
    //         cinemachineThirdPersonFollow = cinemachineCamera.GetComponent<CinemachineThirdPersonFollow>();
    //     }


    //     protected override void Update()
    //     {
    //         base.Update();
    //         if (pInpM.toggleTpsCameraSideAction.WasPressedThisFrame()) { toggleTpsCameraSide(); }
    //     }


    //     protected override void LateUpdate()
    //     {
    //         base.LateUpdate();
    //         cinemachineThirdPersonFollow.CameraSide = Mathf.Lerp(cinemachineThirdPersonFollow.CameraSide, currentCameraSide, 10 * Time.deltaTime);
    //     }


    //     override protected void handleBodyRotation()
    //     {
    //         Vector3 moveDirection = playerMovementController.moveDirection;
    //         bool isPlayerMoving = moveDirection != Vector3.zero;

    //         if (isPlayerMoving)
    //         {
    //             GameObject refBodyRot = new GameObject("RefRotBody");
    //             refBodyRot.transform.position = body.transform.position;
    //             Vector3 targetPos = body.transform.position + moveDirection * 100.0f;
    //             refBodyRot.transform.LookAt(targetPos);

    //             body.transform.rotation = Quaternion.Slerp(
    //                 body.transform.rotation,
    //                 refBodyRot.transform.rotation,
    //                 15.0f * Time.deltaTime
    //             );

    //             Destroy(refBodyRot);
    //         }
    //         if (playerCombatManager.isAiming)
    //         {
    //             // body.transform.eulerAngles = cameraRotation;
    //             // body.transform.eulerAngles = new Vector3(0.0f, body.transform.eulerAngles.y, body.transform.eulerAngles.z);
    //             Quaternion targetRotation = Quaternion.Euler(0.0f, cameraRotation.y, 0.0f);
    //             body.transform.rotation = targetRotation;
    //         }
    //     }


    //     public void toggleTpsCameraSide()
    //     {
    //         if (currentCameraSide == 1.0f) { currentCameraSide = 0f; }
    //         else { currentCameraSide = 1.0f; }
    // }
}
