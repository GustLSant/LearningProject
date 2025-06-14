using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    [HideInInspector] public bool isAiming = false;


    void Start()
    {
        playerInputManager = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();
    }


    void Update()
    {
        getInputs();
    }


    void getInputs()
    {
        isAiming = playerInputManager.aimingInput;
    }
}
