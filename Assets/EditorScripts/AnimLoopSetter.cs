using UnityEngine;
using UnityEditor;

public class LoopAllAnimationsWithPose : MonoBehaviour
{
    [MenuItem("Tools/Set All FBX Animations To Loop + Loop Pose")]
    static void SetAllFBXAnimationsToLoopAndPose()
    {
        string path = EditorUtility.OpenFilePanel("Select FBX", "Assets", "fbx");
        if (string.IsNullOrEmpty(path)) return;

        // Converte o caminho absoluto para o caminho relativo dentro da Unity
        string assetPath = "Assets" + path.Substring(Application.dataPath.Length);

        ModelImporter importer = AssetImporter.GetAtPath(assetPath) as ModelImporter;
        if (importer != null)
        {
            ModelImporterClipAnimation[] clips = importer.clipAnimations;

            for (int i = 0; i < clips.Length; i++)
            {
                clips[i].loopTime = true;
                clips[i].loopPose = true;  // Aqui ativa o Loop Pose
            }

            importer.clipAnimations = clips;
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            Debug.Log($"Todas as animações em {assetPath} agora têm Loop Time e Loop Pose ativados.");
        }
        else
        {
            Debug.LogWarning("O arquivo selecionado não é um FBX válido ou não tem animações.");
        }
    }
}
