using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class FpsController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pivotRot;
    [SerializeField] private GameObject body;
    Vector2 rotInput;
    [HideInInspector] public Vector2 cameraRotation; // eh public para fazer a sincronizacao da rotacao das camera apos trocar o tipo

    [Header("Camera Sensitivity")]
    public float hLookSensi = 0.3f;
    public float vLookSensi = 0.3f;


    void LateUpdate()
    {
        handleCameraRotation();
        handleBodyRotation();
    }


    void handleCameraRotation()
    {
        cameraRotation.x -= rotInput.y * vLookSensi;
        cameraRotation.y += rotInput.x * hLookSensi;
        cameraRotation.x = Math.Clamp(cameraRotation.x, -80.0f, 80.0f);

        pivotRot.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0f);
    }


    void handleBodyRotation()
    {
        body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, cameraRotation.y, body.transform.eulerAngles.z);
    }


    public void syncCameraRotation(Vector2 _newRot)
    {
        cameraRotation = _newRot;
        body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, cameraRotation.y, body.transform.eulerAngles.z);
    }


    void OnLook(InputValue _value)
    {
        rotInput = _value.Get<Vector2>();
    }
}
