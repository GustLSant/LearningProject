using System;
using UnityEngine;


public class FpsController : PlayerCameraController
{
    override protected void handleBodyRotation()
    {
        body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, cameraRotation.y, body.transform.eulerAngles.z);
    }
}
