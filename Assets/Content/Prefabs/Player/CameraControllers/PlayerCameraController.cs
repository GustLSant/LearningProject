using System;
using UnityEngine;


public abstract class PlayerCameraController : MonoBehaviour
{
    protected PlayerInputManager pInpM;

    [HideInInspector] public GameObject pivotRot;
    protected GameObject pivotSway;
    protected GameObject pivotRecoil;
    protected GameObject pivotShake;
    protected GameObject cameraObject;
    protected Camera cameraComponent;
    [SerializeField] protected GameObject body;

    protected PlayerMovementController playerMovementController;
    protected PlayerJumpController playerJumpController;
    protected PlayerCameraManager playerCameraManager;
    protected PlayerCombatController playerCombatController;

    protected Vector2 rotInput;
    [HideInInspector] public Vector2 cameraRotation; // eh public para fazer a sincronizacao da rotacao das camera apos trocar o tipo


    protected virtual void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pInpM = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();

        pivotRot = transform.Find("PivotRot").gameObject;
        pivotSway = transform.Find("PivotRot/PivotSway").gameObject;
        pivotRecoil = transform.Find("PivotRot/PivotSway/PivotRecoil").gameObject;
        pivotShake = transform.Find("PivotRot/PivotSway/PivotRecoil/PivotShake").gameObject;
        cameraObject = transform.Find("PivotRot/PivotSway/PivotRecoil/PivotShake/Camera").gameObject;
        cameraComponent = cameraObject.GetComponent<Camera>();

        playerMovementController = player.GetComponent<PlayerMovementController>();
        playerJumpController = player.GetComponent<PlayerJumpController>();
        playerCameraManager = player.GetComponent<PlayerCameraManager>();
        playerCombatController = player.GetComponent<PlayerCombatController>();
    }


    protected virtual void LateUpdate()
    {
        getInputValues();
        handleCameraRotation();
        handleBodyRotation();
        handleAimEffect();
        handleSwayEffect();
    }


    void handleCameraRotation()
    {
        float isAimingSpeedMultiplier = 1.0f - Convert.ToInt32(playerCombatController.isAiming) * 0.5f;
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
        if(body){ body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, cameraRotation.y, body.transform.eulerAngles.z); }
    }


    void handleSwayEffect()
    {
        int isPlayerStandingStillInt = Convert.ToInt32(playerMovementController.moveDirection == Vector3.zero);
        int isPlayerMoving = Convert.ToInt32(playerMovementController.moveDirection != Vector3.zero);
        int isPlayerSprintingInt = Convert.ToInt32(playerMovementController.isSprinting);

        float msecs = Time.realtimeSinceStartup * 1000f;
        float frequency = 0.015f + (isPlayerSprintingInt * 0.015f);
        float amplitude = 0.05f + (isPlayerSprintingInt * 0.05f);

        Vector3 targetSwayPos = new Vector3(
            Mathf.Sin(msecs * frequency * 0.5f) * amplitude,
            Mathf.Sin(msecs * frequency) * amplitude,
            0.0f
        );
        
        targetSwayPos *= Convert.ToInt32(isPlayerMoving);
        targetSwayPos *= Convert.ToInt32(playerJumpController.isGrounded);

        pivotSway.transform.localPosition = Vector3.Lerp(pivotSway.transform.localPosition, targetSwayPos, 10*Time.deltaTime);
    }


    virtual protected void handleAimEffect()
    {
        float defaultFOV = playerCameraManager.defaultFOV;

        cameraComponent.fieldOfView = Mathf.Lerp(
            cameraComponent.fieldOfView,
            defaultFOV - Convert.ToInt32(playerCombatController.isAiming) * playerCameraManager.aimingFovDecrement,
            12.0f * Time.deltaTime
        );
    }


    public virtual void setIsActive(bool _value)
    {
        gameObject.SetActive(_value);
        cameraObject.SetActive(_value);
    }


    void getInputValues()
    {
        rotInput = pInpM.lookAction.ReadValue<Vector2>();
    }
}
