using System.Collections;
using UnityEngine;


public class PlayerCameraManager : MonoBehaviour
{
    public enum CameraType { TPS, FPS }

    [SerializeField] private GameObject fpsRoot;
    [SerializeField] private GameObject fpsPivotRot;
    [SerializeField] private GameObject tpsRoot;
    [SerializeField] private GameObject tpsPivotRot;

    [HideInInspector] public GameObject currentPivotRot; // eh public para o jogador usar na movimentacao
    [HideInInspector] public CameraType currentCameraMode;


    void Start()
    {
        setCameraType(CameraType.FPS);
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


    void OnChangeCameraType()
    {
        if (currentCameraMode == CameraType.FPS) { setCameraType(CameraType.TPS); }
        else { setCameraType(CameraType.FPS); }
    }
}
