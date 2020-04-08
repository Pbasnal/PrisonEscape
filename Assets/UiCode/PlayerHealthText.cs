using GameCode.Messages;
using GameCode.MessagingFramework;
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
            MessageBus.Register<PlayerHealthUpdateMessage>(OnPlayerHealthUpdate);

            text = GetComponent<TextMeshProUGUI>();
            text.text = "";
        }

        private void OnDestroy()
        {
            MessageBus.Remove<PlayerHealthUpdateMessage>(OnPlayerHealthUpdate);
        }

        private void OnPlayerHealthUpdate(TransportMessage trMsg)
        {
            var msg = trMsg.ConvertTo<PlayerHealthUpdateMessage>();

            text.text = "Health: " + msg.Playerhealth;
        }
    }
}
