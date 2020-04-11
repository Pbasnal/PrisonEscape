using GameCode.Messages;
using GameCode.MessagingFramework;
using TMPro;
using UnityEngine;

namespace UiCode
{
    public class PlayerHealthText : MonoBehaviour
    {
        private TextMeshProUGUI text;

        private void Awake()
        {
            MessageBus.Register<PlayerHealthUpdateMessage>(OnPlayerHealthUpdate);

            text = GetComponent<TextMeshProUGUI>();
            text.text = "Health: 100";
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
