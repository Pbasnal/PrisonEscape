using GameCode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UiCode
{
    //[RequireComponent(typeof(TextMeshPro))]
    public class GameStateText : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public Image Textbackground;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            GameManager.OnGameStateChange += UpdateGameStateText;
            text.text = "";

            Textbackground.color = new Color(0, 0, 0, 0);
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChange -= UpdateGameStateText;
        }

        private void UpdateGameStateText(GameState gameState)
        {
            if (gameState == GameState.Lost)
            {
                text.text = "You are DEAD!!";
                Textbackground.color = new Color(0, 0, 0, 0.7f);
            }
            else if (gameState == GameState.Won)
            {
                text.text = "You made it out ALIVE!!";
                Textbackground.color = new Color(0, 0, 0, 0.7f);
            }
        }
    }
}
