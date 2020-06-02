using UnityEngine;
using UnityEngine.SceneManagement;

namespace LockdownGames.SceneCode
{
    public class FloorLoader : MonoBehaviour
    {
        public string sceneName;
        public LoadSceneMode loadMode;

        public void Start()
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            if (SceneManager.GetSceneByName(sceneName) != null && !scene.isLoaded)
            {
                SceneManager.LoadScene(sceneName, loadMode);
            }
        }
    }
}
