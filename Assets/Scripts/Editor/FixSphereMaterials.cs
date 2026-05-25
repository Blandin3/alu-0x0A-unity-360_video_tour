using UnityEngine;
using UnityEditor;

public class FixSphereMaterials
{
    [MenuItem("Tools/Fix 360 Sphere Materials")]
    static void Fix()
    {
        string[] matNames = { "LivingRoomMaterial", "CantinaMaterial", "CubeMaterial", "MezzanineMaterial" };
        Shader unlit = Shader.Find("Universal Render Pipeline/Unlit");

        foreach (string name in matNames)
        {
            string[] guids = AssetDatabase.FindAssets(name + " t:Material");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
                if (mat == null) continue;

                mat.shader = unlit;
                mat.SetInt("_Cull", 0);
                mat.SetInt("_Surface", 0);
                EditorUtility.SetDirty(mat);
                Debug.Log("Fixed: " + path);
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log("All sphere materials fixed.");
    }
}
