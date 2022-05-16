using Nekonata.SituationCreator.StoryWindow.Controllers.TreeAsset;
using Nekonata.SituationCreator.StoryWindow.Model;
using UnityEngine;

namespace Nekonata.SituationCreator.StoryWindow.Controllers.Facade
{
    public class BehaviourTreeFacade : ScriptableObject
    {
        [SerializeField] private BehaviourTreeAsset _treeAsset;

        public bool TryGetCurrentSelectedNode(out BaseNode node)
        {
            if (_treeAsset == null)
            {
                node = null;
                return false;
            }

            if (_treeAsset.BehaviourTree == null)
            {
                node = null;
                return false;
            }

            node = _treeAsset.BehaviourTree.CurrentSelectedNode;
            return _treeAsset.BehaviourTree.CurrentSelectedNode != null;
        }
    } 
}
