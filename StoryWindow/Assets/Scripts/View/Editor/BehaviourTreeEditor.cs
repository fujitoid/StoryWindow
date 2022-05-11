using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class BehaviourTreeEditor : EditorWindow
{
    private static BehaviourTreeView _behaviourTreeView;
    
    public static void OpenWindow(BehaviourTree behaviourTree)
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        
        _behaviourTreeView.PopulateView(behaviourTree);
        
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/View/Editor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/View/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        _behaviourTreeView = root.Q<BehaviourTreeView>();
    }
}