using System;
using LockdownGames.GameCode.Messages;
using LockdownGames.GameCode.MessagingFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UiCode
{
    public class MenuController : MonoBehaviour
    {
        public GameObject MainMenu;
        public GameObject WinMenu;
        public GameObject LoseMenu;
        public GameObject HealthText;

        public void HideMenu()
        {
            MainMenu.SetActive(false);
            LoseMenu.SetActive(false);
            WinMenu.SetActive(false);
            HealthText.SetActive(false);
        }

        public void PlayGame()
        {
            HideMenu();

            if (SceneManager.GetSceneByName("PlayerMovement").isLoaded)
            {
                SceneManager.UnloadSceneAsync("PlayerMovement");
            }

            if (SceneManager.GetSceneByName("GameplayUi").isLoaded)
            {
                SceneManager.UnloadSceneAsync("GameplayUi");
            }

            HealthText.SetActive(true);
            SceneManager.LoadSceneAsync("GameplayUi", LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync("PlayerMovement", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("Menu");
        }

        public void Quit()
        {
            if (SceneManager.GetSceneByName("LevelGen").isLoaded)
            {
                SceneManager.UnloadSceneAsync("LevelGen");
            }
            Application.Quit();
        }

        private void Awake()
        {
            MessageBus.Register<PlayerHealthUpdateMessage>(OnPlayerhealthUpdate);
            MessageBus.Register<PlayerWonMessage>(OnPlayerWin);

            if (MainMenu == null || LoseMenu == null || WinMenu == null)
            {
                throw new Exception("Set all menu game objects");
            }

            MainMenu.SetActive(true);
            LoseMenu.SetActive(false);
            WinMenu.SetActive(false);
            HealthText.SetActive(false);
        }

        private void OnPlayerWin(TransportMessage msg)
        {
            MainMenu.SetActive(false);
            LoseMenu.SetActive(false);
            WinMenu.SetActive(true);
            HealthText.SetActive(false);
        }

        private void OnPlayerhealthUpdate(TransportMessage trMsg)
        {
            var msg = trMsg.ConvertTo<PlayerHealthUpdateMessage>();

            if (msg == null || !msg.HasPlayerDied)
            {

                return;
            }

            Debug.Log("Player has died. Menu controller");
            MainMenu.SetActive(false);
            LoseMenu.SetActive(true);
            WinMenu.SetActive(false);
            HealthText.SetActive(false);
        }

        private void OnDestroy()
        {
            MessageBus.Remove<PlayerHealthUpdateMessage>(OnPlayerhealthUpdate);
            MessageBus.Remove<PlayerWonMessage>(OnPlayerWin);
        }
    }
}
