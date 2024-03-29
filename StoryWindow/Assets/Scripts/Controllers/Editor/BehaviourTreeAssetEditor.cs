using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nekonata.SituationCreator.StoryWindow.Controllers.TreeAsset.Editor
{
    [CustomEditor(typeof(BehaviourTreeAsset))]
    public class BehaviourTreeAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BehaviourTreeAsset asset = (BehaviourTreeAsset)target;

            if (GUILayout.Button("Create Tree"))
            {
                asset.CreateTree();
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Load tree"))
            {
                asset.LoadTree();
            }

            GUI.enabled = asset.BehaviourTree != null;

            if (GUILayout.Button("Save Tree"))
            {
                asset.SaveTree();
            }

            GUI.enabled = true;

            GUILayout.EndHorizontal();

            GUI.enabled = asset.BehaviourTree != null;

            if (GUILayout.Button("Open Story Window"))
            {
                asset.LoadTree();
                asset.OpenStoryWindow();
            }
        }
    } 
}
