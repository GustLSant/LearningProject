#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ToggleSceneView : MonoBehaviour
{
    void Update()
    {
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F1)) // Ou outro atalho que quiser
            {
                EditorApplication.ExecuteMenuItem("Window/General/Scene");
            }
        #endif
    }
}
