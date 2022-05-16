using Nekonata.SituationCreator.StoryWindow.Controllers.TreeAsset.Context;
using Nekonata.SituationCreator.StoryWindow.Model;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace Nekonata.SituationCreator.StoryWindow.View.Editor
{
    public class BehaviourTreeEditor : EditorWindow
    {
        private static BehaviourTreeView _behaviourTreeView;
        private static IBehaviourTreeSaver _treeSaver;

        public static void OpenWindow(BehaviourTree behaviourTree, IBehaviourTreeSaver treeSaver)
        {
            _treeSaver = treeSaver;
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

            var saveButton = root.Q<Button>("save-button");

            if (_treeSaver == null)
                return;

            saveButton.clicked += _treeSaver.Save;
        }
    } 
}