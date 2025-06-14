using System.Collections;
using UnityEngine;


public class PlayerCameraManager : MonoBehaviour
{
    public enum CameraType { TPS, FPS }

    PlayerInputManager pInpM;

    [SerializeField] private GameObject fpsRoot;
    [SerializeField] private GameObject fpsPivotRot;
    [SerializeField] private GameObject tpsRoot;
    [SerializeField] private GameObject tpsPivotRot;

    [HideInInspector] public GameObject currentPivotRot; // eh public para o jogador usar na movimentacao
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
        print(pInpM);

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
            currentPivotRot = fpsPivotRot;
            fpsRoot.SetActive(true);
            tpsRoot.SetActive(false);
            fpsRoot.GetComponent<FpsController>().syncCameraRotation(tpsRoot.GetComponent<TpsController>().cameraRotation);
        }
        else
        {
            currentCameraMode = CameraType.TPS;
            currentPivotRot = tpsPivotRot;
            fpsRoot.SetActive(false);
            tpsRoot.SetActive(true);
            tpsRoot.GetComponent<TpsController>().syncCameraRotation(fpsRoot.GetComponent<FpsController>().cameraRotation);
        }
    }


    public void changeCameraType()
    {
        if (currentCameraMode == CameraType.FPS) { setCameraType(CameraType.TPS); }
        else { setCameraType(CameraType.FPS); }
    }
}
