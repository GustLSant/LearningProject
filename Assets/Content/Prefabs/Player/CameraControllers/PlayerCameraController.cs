using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCameraController : MonoBehaviour
{
    protected PlayerInputManager pInpM;

    [SerializeField] protected GameObject pivotRot;
    [SerializeField] protected GameObject body;
    [SerializeField] protected GameObject cinemachineGameObject;

    protected PlayerMovementController playerMovementController;
    protected PlayerCameraManager playerCameraManager;
    protected PlayerCombatController playerCombatManager;
    protected CinemachineCamera cinemachineCamera;

    protected Vector2 rotInput;
    [HideInInspector] public Vector2 cameraRotation; // eh public para fazer a sincronizacao da rotacao das camera apos trocar o tipo


    protected virtual void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pInpM = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();

        playerMovementController = player.GetComponent<PlayerMovementController>();
        playerCameraManager = player.GetComponent<PlayerCameraManager>();
        playerCombatManager = player.GetComponent<PlayerCombatController>();
        cinemachineCamera = cinemachineGameObject.GetComponent<CinemachineCamera>();
    }


    protected virtual void LateUpdate()
    {
        getInputValues();
        handleCameraRotation();
        handleBodyRotation();
        handleAimState();
    }


    virtual protected void handleCameraRotation()
    {
        float isAimingSpeedMultiplier = 1.0f - Convert.ToInt32(playerCombatManager.isAiming) * 0.5f;
        cameraRotation.x -= rotInput.y * S_GameSettings.instance.vLookSensi * isAimingSpeedMultiplier;
        cameraRotation.y += rotInput.x * S_GameSettings.instance.hLookSensi * isAimingSpeedMultiplier;
        cameraRotation.x = Math.Clamp(cameraRotation.x, -80.0f, 80.0f);

        pivotRot.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
    }


    virtual protected void handleBodyRotation()
    {
        return;
    }


    public void syncCameraRotation(Vector2 _newRot)
    {
        cameraRotation = _newRot;
        body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, cameraRotation.y, body.transform.eulerAngles.z);
    }


    virtual protected void handleAimState()
    {
        float defaultFOV = playerCameraManager.defaultFOV;

        cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(
            cinemachineCamera.Lens.FieldOfView,
            defaultFOV - Convert.ToInt32(playerCombatManager.isAiming) * playerCameraManager.aimingFovDecrement,
            12.0f * Time.deltaTime
        );
    }


    void getInputValues()
    {
        rotInput = pInpM.lookAction.ReadValue<Vector2>();
    }
}
