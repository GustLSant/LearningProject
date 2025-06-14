using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] protected GameObject pivotRot;
    [SerializeField] protected GameObject body;
    [SerializeField] protected GameObject cinemachineGameObject;

    PlayerInputManager playerInputManager;
    protected PlayerMovementController playerMovementController;
    protected PlayerCameraManager playerCameraManager;
    private PlayerCombatController playerCombatManager;
    protected CinemachineCamera cinemachineCamera;

    protected Vector2 rotInput;
    [HideInInspector] public Vector2 cameraRotation; // eh public para fazer a sincronizacao da rotacao das camera apos trocar o tipo

    protected bool isPlayerAiming = false;


    protected virtual void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameObject player = GameObject.FindWithTag("Player");

        playerInputManager = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();
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
        float isAimingSpeedMultiplier = 1.0f - Convert.ToInt32(isPlayerAiming) * 0.5f;
        cameraRotation.x -= rotInput.y * playerCameraManager.vLookSensi * isAimingSpeedMultiplier;
        cameraRotation.y += rotInput.x * playerCameraManager.hLookSensi * isAimingSpeedMultiplier;
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
        rotInput = playerInputManager.lookRotInput;
        isPlayerAiming = playerCombatManager.isAiming;
    }
}
