using UnityEngine;

public class PlayerJumpManager : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] private float gravityAccel = 10.0f;
    [SerializeField] private float jumpStrength = 5.0f;
    public float currentVerticalMovement = 0.0f; // eh public para o componente de movimentacao conseguir acessar


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        if (controller.isGrounded && currentVerticalMovement < 0.0f) { currentVerticalMovement = 0.0f; }
        else { currentVerticalMovement -= gravityAccel * Time.fixedDeltaTime; }
    }

    public void OnPlayerJump()
    {
        if (controller.isGrounded) { currentVerticalMovement = jumpStrength; } // o problema eh q as vezes o isGrounded nao funciona
    }
}
