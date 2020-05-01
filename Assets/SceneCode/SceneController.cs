using UnityEngine;
using UnityEngine.SceneManagement;

namespace LockdownGames.SceneCode
{
    public class SceneController : MonoBehaviour
    {
        public void Start()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        }
    }
}
