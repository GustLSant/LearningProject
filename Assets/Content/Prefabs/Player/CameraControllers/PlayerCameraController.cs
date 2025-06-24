using System;
using UnityEngine;


public abstract class PlayerCameraController : MonoBehaviour
{
    protected PlayerInputManager pInpM;

    [HideInInspector] public GameObject pivotRot;
    protected GameObject pivotSway;
    protected GameObject pivotRecoil;
    protected GameObject pivotShake;
    protected Camera cameraComponent;
    [SerializeField] protected GameObject body;

    protected PlayerMovementController playerMovementController;
    protected PlayerCameraManager playerCameraManager;
    protected PlayerCombatController playerCombatManager;

    protected Vector2 rotInput;
    [HideInInspector] public Vector2 cameraRotation; // eh public para fazer a sincronizacao da rotacao das camera apos trocar o tipo


    protected virtual void Awake()
    {
        Debug.Log("Meu transform é: " + gameObject.name);
        Debug.Log("Filhos diretos:");
        foreach(Transform child in transform)
        {
            Debug.Log("- " + child.name);
        }


        GameObject player = GameObject.FindWithTag("Player");
        pInpM = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();

        pivotRot = transform.Find("PivotRot").gameObject;
        pivotSway = transform.Find("PivotRot/PivotSway").gameObject;
        pivotShake = transform.Find("PivotRot/PivotSway/PivotShake").gameObject;
        cameraComponent = transform.Find("PivotRot/PivotSway/PivotShake/Camera").GetComponent<Camera>();

        playerMovementController = player.GetComponent<PlayerMovementController>();
        playerCameraManager = player.GetComponent<PlayerCameraManager>();
        playerCombatManager = player.GetComponent<PlayerCombatController>();
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
        if(body){ body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, cameraRotation.y, body.transform.eulerAngles.z); }
    }


    void handleSwayEffect()
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


    virtual protected void handleAimEffect()
    {
        float defaultFOV = playerCameraManager.defaultFOV;

        cameraComponent.fieldOfView = Mathf.Lerp(
            cameraComponent.fieldOfView,
            defaultFOV - Convert.ToInt32(playerCombatManager.isAiming) * playerCameraManager.aimingFovDecrement,
            12.0f * Time.deltaTime
        );
    }


    void getInputValues()
    {
        rotInput = pInpM.lookAction.ReadValue<Vector2>();
    }
}
