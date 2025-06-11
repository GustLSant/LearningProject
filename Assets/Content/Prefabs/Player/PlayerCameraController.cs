using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCameraController : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    [SerializeField] protected GameObject player;
    [SerializeField] protected GameObject pivotRot;
    [SerializeField] protected GameObject body;
    [SerializeField] protected GameObject cinemachineGameObject;
    protected CinemachineCamera cinemachineCamera;
    protected PlayerCameraManager playerCameraManager;
    protected Vector2 rotInput;
    [HideInInspector] public Vector2 cameraRotation; // eh public para fazer a sincronizacao da rotacao das camera apos trocar o tipo

    private PlayerCombatManager playerCombatManager;
    protected bool isPlayerAiming = false;


    protected void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerInputManager = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();
        playerCameraManager = player.GetComponent<PlayerCameraManager>();
        playerCombatManager = player.GetComponent<PlayerCombatManager>();
        cinemachineCamera = cinemachineGameObject.GetComponent<CinemachineCamera>();
    }


    void LateUpdate()
    {
        isPlayerAiming = playerCombatManager.isAiming;
        getInputValues();
        handleCameraRotation();
        handleBodyRotation();
        handleAimState();
    }


    virtual protected void handleCameraRotation()
    {
        return;
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
    }
}
