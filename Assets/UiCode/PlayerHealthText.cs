using GameAi.Code.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UiCode
{
    public class PlayerHealthText : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public Image Textbackground;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            Player.OnPlayerHealthUpdate += UpdateHealthText;
            text.text = "";
        }

        private void OnDestroy()
        {
            Player.OnPlayerHealthUpdate -= UpdateHealthText;
        }

        private void UpdateHealthText(int health)
        {
            text.text = "Health: " + health;
        }
    }
}
