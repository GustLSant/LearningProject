using System;
using UnityEngine;


public class FpsController : PlayerCameraController
{
    [SerializeField] GameObject pivotSway;


    protected override void Update()
    {
        base.Update();
        handleAimSway();
    }


    override protected void handleBodyRotation(){}


    void handleAimSway()
    {
        int isPlayerStandingStillInt = Convert.ToInt32(playerMovementController.moveDirection == Vector3.zero);
        int isPlayerSprintingInt = Convert.ToInt32(playerMovementController.isSprinting);

        float msecs = Time.realtimeSinceStartup * 1000f;
        float frequency = 0.02f; //+ (isPlayerSprintingInt * 0.02f);
        float amplitude = 0.05f + (isPlayerSprintingInt * 0.05f);

        Vector3 targetSwayPos = new Vector3(
            Mathf.Sin(msecs * frequency * 0.5f) * amplitude,
            Mathf.Sin(msecs * frequency) * amplitude,
            0.0f
        );
        
        targetSwayPos *= 1.0f - isPlayerStandingStillInt;

        pivotSway.transform.localPosition = Vector3.Lerp(pivotSway.transform.localPosition, targetSwayPos, 10*Time.deltaTime);
    }
}
