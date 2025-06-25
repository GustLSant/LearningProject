using System.Collections;
using UnityEngine;


public class PlayerCameraManager : MonoBehaviour
{
    public enum CameraType { TPS, FPS }

    PlayerInputManager pInpM;

    FpsCameraController fpsCameraController;
    TpsCameraController tpsCameraController;

    [HideInInspector] public GameObject currentPivotRot;
    [HideInInspector] public CameraType currentCameraMode;

    [Header("Camera Sensitivity")]
    public float hLookSensi = 0.3f;
    public float vLookSensi = 0.3f;

    [Header("Default FOV")]
    public float defaultFOV = 75.0f;
    public float aimingFovDecrement = 25.0f;


    void Awake()
    {
        pInpM = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();

        fpsCameraController = GameObject.FindGameObjectWithTag("PlayerFpsCameraController").GetComponent<FpsCameraController>();
        tpsCameraController = GameObject.FindGameObjectWithTag("PlayerTpsCameraController").GetComponent<TpsCameraController>();

        setCameraType(CameraType.FPS);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if(pInpM.changeCameraModeAction.WasPressedThisFrame()){ changeCameraType(); }
    }


    void setCameraType(CameraType _newCameraType)
    {
        if (_newCameraType == CameraType.FPS)
        {
            currentCameraMode = CameraType.FPS;
            currentPivotRot = fpsCameraController.pivotRot;
            fpsCameraController.setIsActive(true);
            tpsCameraController.setIsActive(false);
            fpsCameraController.syncCameraRotation(tpsCameraController.cameraRotation);
        }
        else
        {
            currentCameraMode = CameraType.TPS;
            currentPivotRot = tpsCameraController.pivotRot;
            fpsCameraController.setIsActive(false);
            tpsCameraController.setIsActive(true);
            tpsCameraController.syncCameraRotation(fpsCameraController.cameraRotation);
        }
    }


    public void changeCameraType()
    {
        if (currentCameraMode == CameraType.FPS) { setCameraType(CameraType.TPS); }
        else { setCameraType(CameraType.FPS); }
    }
}
