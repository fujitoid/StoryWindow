using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class BehaviourTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }

    private BehaviourTree _tree;
    
    public BehaviourTreeView()
    {
        Insert(0, new GridBackground());
        
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/View/Editor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }

    public void PopulateView(BehaviourTree tree)
    {
        this._tree = tree;
        
        DeleteElements(graphElements);
        
        foreach (var node in tree.Nodes)
        {
            CreateNodeView(node);
        }
    }

    private void CreateNodeView(BaseNode node)
    {
        
    }
}
