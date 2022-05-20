using Nekonata.SituationCreator.StoryWindow.Controllers.TreeAsset;
using Nekonata.SituationCreator.StoryWindow.View.Runtime;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nekonata.SituationCreator.StoryWindow.Controllers
{
    public class BehaviourTreeRuntimeInitializer : MonoBehaviour, ICoroutineProvider
    {
        [SerializeField] private BehaviourTreeAsset _behaviourTreeAsset;
        [SerializeField] private UIDocument _document;

        private BehaviourTreeView _treeView;

        private void Start()
        {
            _treeView = _document.rootVisualElement.Q<BehaviourTreeView>();
            _behaviourTreeAsset.LoadTree();
            _treeView.PopulateView(_behaviourTreeAsset.BehaviourTree);
            _treeView.SetContext(_behaviourTreeAsset, this);
        }

        void ICoroutineProvider.StartCoroutine(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
    } 
}
