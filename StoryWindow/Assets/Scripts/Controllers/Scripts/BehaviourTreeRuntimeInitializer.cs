using Nekonata.SituationCreator.StoryWindow.Controllers.TreeAsset;
using Nekonata.SituationCreator.StoryWindow.View.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nekonata.SituationCreator.StoryWindow.Controllers
{
    public class BehaviourTreeRuntimeInitializer : MonoBehaviour
    {
        [SerializeField] private BehaviourTreeAsset _behaviourTreeAsset;
        [SerializeField] private UIDocument _document;

        private BehaviourTreeView _treeView;

        private void Start()
        {
            _treeView = _document.rootVisualElement.Q<BehaviourTreeView>();
            _behaviourTreeAsset.LoadTree();
            _treeView.PopulateView(_behaviourTreeAsset.BehaviourTree);
        }
    } 
}
