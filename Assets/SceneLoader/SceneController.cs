using UnityEngine;
using UnityEngine.SceneManagement;

namespace UiCode
{
    public class SceneController : MonoBehaviour
    {
        public void Start()
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        }
    }
}
