using LockdownGames.GameCode.Messages;
using LockdownGames.GameCode.MessagingFramework;

using UnityEngine;
using UnityEngine.UI;

namespace UiCode
{
    public class PlayerHealthBar : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            MessageBus.Register<PlayerHealthUpdateMessage>(OnPlayerHealthUpdate);

            slider = GetComponent<Slider>();
        }

        private void OnDestroy()
        {
            MessageBus.Remove<PlayerHealthUpdateMessage>(OnPlayerHealthUpdate);
        }

        private void OnPlayerHealthUpdate(TransportMessage trMsg)
        {
            var msg = trMsg.ConvertTo<PlayerHealthUpdateMessage>();

            slider.value = msg.Playerhealth;
        }
    }
}
