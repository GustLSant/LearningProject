using System;
using UnityEngine;


[DisallowMultipleComponent]
public class TpsCameraController : PlayerCameraController
{
    GameObject fixedPivotRot; // para a rotacao do corpo e efeito de delay na posicao da camera
    readonly Vector3 DEFAULT_CAMERA_POS = new Vector3(1.0f, 0.5f, -2.75f);

    float currentCameraSide = 1.0f;
    float targetCameraSide = 1.0f;


    protected override void Awake()
    {
        base.Awake();
        fixedPivotRot = transform.Find("FixedPivotRot").gameObject;

        pivotRot.transform.SetParent(null, true);
    }


    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (pInpM.toggleTpsCameraSideAction.WasPressedThisFrame()) { toggleTpsCameraSide(); }
        handleCameraSideInterpolation();
        handleCameraCollision();
    }

    void FixedUpdate()
    {
        handleCameraDelayEffect();
    }


    void handleCameraDelayEffect()
    {
        pivotRot.transform.position = Vector3.Lerp(
            pivotRot.transform.position,
            fixedPivotRot.transform.position,
            10.0f * Time.fixedDeltaTime
        );
    }


    void handleCameraCollision()
    {
        Vector3 targetPosition = DEFAULT_CAMERA_POS;
        targetPosition.x *= currentCameraSide;

        cameraObject.transform.localPosition = targetPosition;

        Vector3 origin = pivotRot.transform.position;
        Vector3 direction = (cameraObject.transform.position - pivotRot.transform.position).normalized; // direcao do pivotRot ate a camera, em coordenadas globais
        float maxDistance = DEFAULT_CAMERA_POS.magnitude;
        float radius = 0.2f;

        if (Physics.SphereCast(origin, radius, direction, out RaycastHit hit, maxDistance))
        {
            float safePercent = (hit.distance - 0.1f) / maxDistance;
            cameraObject.transform.localPosition = targetPosition * safePercent;
        }
    }


    override protected void handleBodyRotation()
    {
        Vector3 moveDirection = playerMovementController.moveDirection;
        bool isPlayerMoving = moveDirection != Vector3.zero;

        if (isPlayerMoving)
        {
            Vector3 targetPos = body.transform.position + moveDirection * 100.0f;
            fixedPivotRot.transform.LookAt(targetPos);

            body.transform.rotation = Quaternion.Slerp(
                body.transform.rotation,
                fixedPivotRot.transform.rotation,
                15.0f * Time.deltaTime
            );
        }
        if (playerCombatController.isAiming)
        {
            Quaternion targetRotation = Quaternion.Euler(0.0f, cameraRotation.y, 0.0f);
            body.transform.rotation = targetRotation;
        }
    }


    void handleCameraSideInterpolation()
    {
        currentCameraSide = Mathf.Lerp(currentCameraSide, targetCameraSide, 10 * Time.deltaTime);
    }


    public void toggleTpsCameraSide()
    {
        targetCameraSide *= -1.0f;
    }


    public override void setIsActive(bool _value)
    {
        base.setIsActive(_value);
        body.SetActive(_value);
        if (_value == true) { pivotRot.transform.position = fixedPivotRot.transform.position; }
    }
}
