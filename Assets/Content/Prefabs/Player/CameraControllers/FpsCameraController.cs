using System;
using UnityEngine;


[DisallowMultipleComponent]
public class FpsCameraController : PlayerCameraController
{
    [SerializeField] private GameObject bodyPosRef;


    protected override void Awake()
    {
        base.Awake();
        body.transform.SetParent(null, true);
    }


    void Update()
    {
        handleBodyPosition();
    }


    void FixedUpdate()
    {
        handleBodyYDelayEffect();
    }


    override protected void handleBodyRotation()
    {
        body.transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0.0f);
    }


    void handleBodyPosition()
    {
        Vector3 newPosition = new Vector3(
            pivotRot.transform.position.x,
            body.transform.position.y,
            pivotRot.transform.position.z
        );
        body.transform.position = newPosition; // posicao com o Y desatualizado
    }

    void handleBodyYDelayEffect()
    {
        // atualizacao do Y com delay
        body.transform.position = Vector3.Lerp(
            body.transform.position,
            bodyPosRef.transform.position,
            12 * Time.deltaTime
        );

        // limitacao pra os bracos nao subirem muito
        Vector3 clampedPosition = body.transform.position;
        clampedPosition.y = Mathf.Clamp(
            clampedPosition.y,
            bodyPosRef.transform.position.y - 0.05f,
            bodyPosRef.transform.position.y + 0.025f
        );

        body.transform.position = clampedPosition;
    }
}
