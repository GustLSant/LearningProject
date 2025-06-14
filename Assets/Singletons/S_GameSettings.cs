using UnityEngine;

public class S_GameSettings : MonoBehaviour
{
    public static S_GameSettings instance;

    [HideInInspector] public float vLookSensi = 0.3f;
    [HideInInspector] public float hLookSensi = 0.3f;

    [HideInInspector] public bool isSprintHoldMode = false;
    [HideInInspector] public bool isAimHoldMode = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Faz o objeto sobreviver entre cenas
        }
        else { Destroy(gameObject); }
    }
}
