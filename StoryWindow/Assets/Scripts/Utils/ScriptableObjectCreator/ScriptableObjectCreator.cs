#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectCreator : Editor
{
    [MenuItem("Assets/Create/MyTools/Create this ScriptableObject")]
    public static void CreateThisScriptableObject()
    {
        var selection = Selection.objects.FirstOrDefault();

        var selectionPath = AssetDatabase.GetAssetPath(selection);

        var directory = Path.GetDirectoryName(selectionPath);
        var fileName = Path.GetFileNameWithoutExtension(selectionPath);
        var newFilePath = Path.Combine(directory, $"New{fileName}.asset");

        var assetPath = AssetDatabase.GenerateUniqueAssetPath(newFilePath);
        var so = ScriptableObject.CreateInstance((selection as MonoScript).GetClass());

        AssetDatabase.CreateAsset(so, assetPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif