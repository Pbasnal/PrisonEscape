using System;
using System.Collections.Generic;
using LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace LockdownGames.GameCode.MessagingFramework
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
                    Debug.Log(ErrorMessageForMissingBus);
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

            AllConditions.Instance.Reset();
        }

        public static void Register<T>(UnityAction<TransportMessage> messageHandler)
        {
            if (!instance._messageDictionary.TryGetValue(typeof(T), out var handlerEvent))
            {
                handlerEvent = new HandlerEvent();
                instance._messageDictionary.Add(typeof(T), handlerEvent);
            }

            handlerEvent.AddListener(messageHandler);
        }

        public static void Remove<T>(UnityAction<TransportMessage> messageHandler)
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

        public static void Publish<T>(T message)
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
