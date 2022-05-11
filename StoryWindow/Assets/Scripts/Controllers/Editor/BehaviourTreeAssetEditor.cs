using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BehaviourTreeAsset))]
public class BehaviourTreeAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BehaviourTreeAsset asset = (BehaviourTreeAsset) target;

        asset.IsLoaded = asset.BehaviourTree != null;

        if (GUILayout.Button("Create Tree"))
        {
            asset.CreateTree();
        }

        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Load tree"))
        {
            asset.LoadTree();
        }

        if (GUILayout.Button("Save Tree"))
        {
            asset.SaveTree();
        }
        
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Open Story Window"))
        {
            asset.OpenStoryWindow();
        }
    }
}
