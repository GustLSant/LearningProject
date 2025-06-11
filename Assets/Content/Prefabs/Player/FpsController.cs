using System;
using UnityEngine;


public class FpsController : PlayerCameraController
{
    override protected void handleCameraRotation()
    {
        cameraRotation.x -= rotInput.y * playerCameraManager.vLookSensi;
        cameraRotation.y += rotInput.x * playerCameraManager.hLookSensi;
        cameraRotation.x = Math.Clamp(cameraRotation.x, -80.0f, 80.0f);

        pivotRot.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
    }


    override protected void handleBodyRotation()
    {
        body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, cameraRotation.y, body.transform.eulerAngles.z);
    }
}
