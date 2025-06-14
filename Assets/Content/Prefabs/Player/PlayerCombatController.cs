using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    PlayerInputManager pInM;
    [HideInInspector] public bool isAiming = false;


    void Start()
    {
        pInM = GameObject.FindWithTag("PlayerInputManager").GetComponent<PlayerInputManager>();
    }


    void Update()
    {
        getInputs();
    }


    void getInputs()
    {
        isAiming = pInM.aimAction.IsPressed();
    }
}
