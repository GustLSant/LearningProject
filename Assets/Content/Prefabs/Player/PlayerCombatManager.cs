using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatManager : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    public bool isAiming = false;


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
