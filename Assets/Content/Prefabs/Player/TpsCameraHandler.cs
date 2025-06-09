using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class TpsCameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject pivotRot;
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
    }



    void handleCameraRotation()
    {
        cameraRotation.x -= rotInput.y * vLookSensi;
        cameraRotation.y += rotInput.x * hLookSensi;
        cameraRotation.x = Math.Clamp(cameraRotation.x, -80.0f, 80.0f);

        pivotRot.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
    }


    void OnLook(InputValue _value)
    {
        rotInput = _value.Get<Vector2>();
    }
}
