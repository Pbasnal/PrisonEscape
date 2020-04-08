using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameCode.MessagingFramework
{
    public class MessageBus : MonoBehaviour
    {
        private static MessageBus _bus;

        private static string ErrorMessageForMissingBus = "Add Message Bus component to an object";
        private Dictionary<Type, HandlerEvent> _messageDictionary;

        private static MessageBus instance
        {
            get
            {
                if (_bus != null)
                {
                    return _bus;
                }

                _bus = FindObjectOfType<MessageBus>();

                if (_bus == null)
                {
                    Debug.LogWarning(ErrorMessageForMissingBus);
                    return null;
                }

                _bus.Init();

                return _bus;
            }
        }

        private void Init()
        {
            if (_messageDictionary != null)
            {
                return;
            }

            _messageDictionary = new Dictionary<Type, HandlerEvent>();
        }

        public static void Register<T>(UnityAction<TransportMessage> messageHandler) where T : IMessage
        {
            if (!instance._messageDictionary.TryGetValue(typeof(T), out var handlerEvent))
            {
                handlerEvent = new HandlerEvent();
                instance._messageDictionary.Add(typeof(T), handlerEvent);
            }

            handlerEvent.AddListener(messageHandler);
        }

        public static void Remove<T>(UnityAction<TransportMessage> messageHandler) where T : IMessage
        {
            if (instance == null)
            {
                return;
            }

            if (instance._messageDictionary.TryGetValue(typeof(T), out var handlers))
            {
                handlers.RemoveListener((msg) => { });
            }
        }

        public static void Publish<T>(T message) where T : IMessage
        {
            if (instance == null)
            {
                return;
            }

            if (!instance._messageDictionary.TryGetValue(typeof(T), out var handlers))
            {
                return;
            }

            var tmsg = new TransportMessage
            {
                message = message
            };
            
            handlers?.Invoke(tmsg);
        }

        private void OnDestroy()
        {
            ErrorMessageForMissingBus = "Message Bus has been destroyed.";
        }
    }
}
