using System;
using UnityEngine;
using Unity.Cinemachine;


public class TpsController : PlayerCameraController
{
    [SerializeField] GameObject cinemachineCamera;
    CinemachineThirdPersonFollow cinemachineThirdPersonFollow;
    float currentCameraSide = 1.0f;


    void Start()
    {
        base.Start();
        cinemachineThirdPersonFollow = cinemachineCamera.GetComponent<CinemachineThirdPersonFollow>();
    }


    void Update()
    {
        cinemachineThirdPersonFollow.CameraSide = Mathf.Lerp(cinemachineThirdPersonFollow.CameraSide, currentCameraSide, 10 * Time.deltaTime);
    }


    override protected void handleCameraRotation()
    {
        cameraRotation.x -= rotInput.y * playerCameraManager.vLookSensi;
        cameraRotation.y += rotInput.x * playerCameraManager.hLookSensi;
        cameraRotation.x = Math.Clamp(cameraRotation.x, -80.0f, 80.0f);

        pivotRot.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
    }


    override protected void handleBodyRotation()
    {
        Vector3 moveDirection = player.GetComponent<PlayerMovement>().moveDirection;
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
        }
    }


    void OnToggleTpsCameraSide()
    {
        if (currentCameraSide == 1.0f) { currentCameraSide = 0f; }
        else { currentCameraSide = 1.0f; }
    }
}
