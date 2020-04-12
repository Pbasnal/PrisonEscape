using GameCode.Messages;
using GameCode.MessagingFramework;
using System;
using UnityEngine;

namespace UiCode
{
    public class MenuTesting : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                MessageBus.Publish(new PlayerWonMessage());
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                MessageBus.Publish(new PlayerHealthUpdateMessage(0));
            }
        }
    }
}
