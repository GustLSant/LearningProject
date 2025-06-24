using System.Collections;
using UnityEngine;


public class PlayerCameraManager : MonoBehaviour
{
    public enum CameraType { TPS, FPS }

    PlayerInputManager pInpM;

    GameObject fpsRoot;
    GameObject tpsRoot;
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

        fpsRoot = GameObject.FindGameObjectWithTag("PlayerFpsCameraController");
        tpsRoot = GameObject.FindGameObjectWithTag("PlayerTpsCameraController");
        fpsCameraController = fpsRoot.GetComponent<FpsCameraController>();
        tpsCameraController = tpsRoot.GetComponent<TpsCameraController>();

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
            fpsRoot.SetActive(true);
            tpsRoot.SetActive(false);
            fpsCameraController.syncCameraRotation(tpsCameraController.cameraRotation);
        }
        else
        {
            currentCameraMode = CameraType.TPS;
            currentPivotRot = tpsCameraController.pivotRot;
            fpsRoot.SetActive(false);
            tpsRoot.SetActive(true);
            tpsCameraController.syncCameraRotation(fpsCameraController.cameraRotation);
        }
    }


    public void changeCameraType()
    {
        if (currentCameraMode == CameraType.FPS) { setCameraType(CameraType.TPS); }
        else { setCameraType(CameraType.FPS); }
    }
}
