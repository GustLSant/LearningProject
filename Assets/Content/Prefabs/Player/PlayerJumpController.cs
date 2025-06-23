using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerJumpController : MonoBehaviour
{
    PlayerInputManager pInpM;

    [SerializeField] private float gravityAccel = 20.0f;
    [SerializeField] private float jumpStrength = 7.5f;
    [HideInInspector] public float currentVerticalMovement = 0.0f;
    public bool isGrounded = false;
    const float GRAVITY_TERMINAL_VELOCITY = -20.0f;


    void Awake()
    {
        pInpM = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();
    }


    void Update()
    {
        if (pInpM.jumpAction.WasPressedThisFrame()) { OnPlayerJump(); }
    }


    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up*0.1f, Vector3.down, 0.11f);
        // if (controller.isGrounded && currentVerticalMovement < 0.0f) { currentVerticalMovement = 0.0f; }
        if (isGrounded) { currentVerticalMovement = 0.0f; }
        else
        {
            currentVerticalMovement -= gravityAccel * Time.fixedDeltaTime;
            currentVerticalMovement = Mathf.Max(currentVerticalMovement, GRAVITY_TERMINAL_VELOCITY);
        }
    }

    public void OnPlayerJump()
    {
        //if (controller.isGrounded) { currentVerticalMovement = jumpStrength; } // o problema eh q as vezes o isGrounded nao funciona
        if (isGrounded) {
            currentVerticalMovement = jumpStrength; 
        }
    }
}
