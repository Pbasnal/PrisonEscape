using GameCode.Interfaces;
using GameCode.Messages;
using GameCode.MessagingFramework;
using System.Collections.Generic;
using UnityEngine;

namespace GameCode.Player
{
    public class UserInputAttack : MonoBehaviour
    {
        public Weapon Weapon;
        public List<float> modifiers;

        private Transform _target;

        private void Awake()
        {
            MessageBus.Register<UserInputBeganMessage>(CaptureAttackLocation);
        }

        private void FixedUpdate()
        {
            if (_target == null || !Weapon.IsTargetInRange(_target.position))
            {
                return;
            }

            Weapon.AddAttackModifiers(modifiers);
            Weapon.Attack(_target);
        }

        private void CaptureAttackLocation(TransportMessage trMsg)
        {
            var msg = trMsg.ConvertTo<UserInputBeganMessage>();
            if (msg == null)
            {
                return;
            }

            _target = msg.ClickOnTransform;
        }

    }
}
