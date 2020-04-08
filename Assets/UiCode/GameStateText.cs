using GameCode.Messages;
using GameCode.MessagingFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UiCode
{
    [RequireComponent(typeof(TextMeshProUGUI ))]
    public class GameStateText : MonoBehaviour
    {
        public TextMeshProUGUI text;
        public Image Textbackground;

        private void Awake()
        {
            MessageBus.Register<PlayerHealthUpdateMessage>(OnPlayerhealthUpdate);
            MessageBus.Register<PlayerWonMessage>(OnPlayerWin);

            text = GetComponent<TextMeshProUGUI>();
            text.text = "";

            Textbackground.color = new Color(0, 0, 0, 0);
        }

        private void OnPlayerWin(TransportMessage msg)
        {
            text.text = "You made it out ALIVE!!";
            Textbackground.color = new Color(0, 0, 0, 0.7f);
        }

        private void OnPlayerhealthUpdate(TransportMessage trMsg)
        {
            var msg = trMsg.ConvertTo<PlayerHealthUpdateMessage>();

            if (msg.HasPlayerDied)
            {
                text.text = "You are DEAD!!";
                Textbackground.color = new Color(0, 0, 0, 0.7f);
            }
        }

        private void OnDestroy()
        {
            MessageBus.Remove<PlayerHealthUpdateMessage>(OnPlayerhealthUpdate);
            MessageBus.Remove<PlayerWonMessage>(OnPlayerWin);
        }
    }
}
