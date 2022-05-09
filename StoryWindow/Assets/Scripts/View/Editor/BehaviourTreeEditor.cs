using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class BehaviourTreeEditor : EditorWindow
{
    private BehaviourTreeView _behaviourTreeView;
    
    [MenuItem("SituationCreator/StoryWindow")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/View/Editor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree();
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/View/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        _behaviourTreeView = root.Q<BehaviourTreeView>();
    }

    private void OnSelectionChange()
    {
        BehaviourTree tree = Selection.activeObject as BehaviourTree;
        if (tree)
        {
            
        }
    }
}