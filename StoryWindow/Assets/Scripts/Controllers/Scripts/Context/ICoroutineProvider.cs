using System.Collections;

namespace Nekonata.SituationCreator.StoryWindow.Controllers
{
    public interface ICoroutineProvider
    {
        public void StartCoroutine(IEnumerator enumerator);
    } 
}
