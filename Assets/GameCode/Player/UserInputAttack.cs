using System.Collections.Generic;

using LockdownGames.GameCode.Messages;
using LockdownGames.GameCode.MessagingFramework;
using LockdownGames.Mechanics.InteractionSystem;

using UnityEngine;

namespace LockdownGames.GameCode.Player
{
    public class UserInputAttack : MonoBehaviour
    {
        public Weapon Weapon;
        public List<float> modifiers;

        private Interactable _target;

        private void Awake()
        {
            MessageBus.Register<UserInputBeganMessage>(CaptureAttackLocation);
        }

        private void FixedUpdate()
        {
            if (_target == null || Vector3.Distance(transform.position, _target.interactionLocation.position) > 0.5f)
            {
                return;
            }

            Debug.Log("Interacting");
            _target.Interact(this);

            _target = null;
            //if (_target == null || !Weapon.IsTargetInRange(_target.transform.position))
            //{
            //    return;
            //}

            //Weapon.AddAttackModifiers(modifiers);
            //Weapon.Attack(_target.transform);
        }

        private void CaptureAttackLocation(TransportMessage trMsg)
        {
            var msg = trMsg.ConvertTo<UserInputBeganMessage>();
            if (msg == null)
            {
                return;
            }

            _target = msg.transformOfClickedObject.GetComponent<Interactable>();
        }

    }
}
