using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class TpsCameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject pivotRot;
    [SerializeField] private GameObject pivotBodyRefRot;
    Vector2 rotInput;
    Vector2 cameraRotation;

    [Header("Camera Sensitivity")]
    public float hLookSensi = 0.3f;
    public float vLookSensi = 0.3f;


    void Start()
    {

    }


    void LateUpdate()
    {
        handleCameraRotation();
        handleRotReferenceForBody();
    }



    void handleCameraRotation()
    {
        cameraRotation.x -= rotInput.y * vLookSensi;
        cameraRotation.y += rotInput.x * hLookSensi;
        cameraRotation.x = Math.Clamp(cameraRotation.x, -80.0f, 80.0f);

        pivotRot.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
    }


    void handleRotReferenceForBody()
    {
        Vector3 targetPos = pivotRot.transform.position + pivotRot.transform.forward * 100.0f;
        pivotBodyRefRot.transform.LookAt(targetPos);
        pivotBodyRefRot.transform.eulerAngles = new Vector3(0, pivotBodyRefRot.transform.eulerAngles.y, 0);
    }


    void OnLook(InputValue _value)
    {
        rotInput = _value.Get<Vector2>();
    }
}
