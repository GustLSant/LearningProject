using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerJumpController : MonoBehaviour
{
    PlayerInputManager pInpM;

    private float gravityAccel = 20.0f;
    private float jumpStrength = 7.5f;
    [HideInInspector] public float currentVerticalMovement = 0.0f;
    [HideInInspector] public bool isGrounded = false;
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
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.15f);

        // Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * 0.11f, Color.red);
        if (isGrounded) { currentVerticalMovement = 0.0f; }
        else
        {
            currentVerticalMovement -= gravityAccel * Time.fixedDeltaTime;
            currentVerticalMovement = Mathf.Max(currentVerticalMovement, GRAVITY_TERMINAL_VELOCITY);
        }
    }

    public void OnPlayerJump()
    {
        if (isGrounded) {
            currentVerticalMovement = jumpStrength; 
        }
    }
}
