using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAi.Code.Player
{
    public class WeaponHolder : MonoBehaviour
    {
        public Weapon Weapon;

        [HideInInspector]public bool HasWeapon => Weapon != null;

        private IDictionary<string, Transform> WeaponHoldingPositions;

        private const string WeaponPositionTag = "WeaponPositions";

        private const string WeaponPosDown = "WeaponPosDown";
        private const string WeaponPosUp = "WeaponPosUp";
        private const string WeaponPosLeft = "WeaponPosLeft";
        private const string WeaponPosRight = "WeaponPosRight";

        public void Awake()
        {
            WeaponHoldingPositions = new Dictionary<string, Transform>();

            foreach (Transform child in transform)
            {
                if (child.tag != WeaponPositionTag) continue;

                WeaponHoldingPositions.Add(child.name, child);
            }

            // Todo: Adding weapon should be managed by a different component
            // to give flexibility of changing weapons during play time
            AddWeapon(Weapon);
        }

        public void AddWeapon(Weapon weapon)
        {
            Weapon = weapon;

            //Weapon.SetActive(false);
        }

        public void Attack(Vector2 dir)
        {
            if (Weapon == null) return;

            if (dir == Vector2.up)
            {
                //Weapon.transform.position = WeaponHoldingPositions[WeaponPosUp].position;
                Weapon.transform.SetPositionAndRotation(
                    WeaponHoldingPositions[WeaponPosUp].position, Quaternion.Euler(0, 0, 180));
            }
            else if (dir == Vector2.down)
            {
                Weapon.transform.SetPositionAndRotation(
                    WeaponHoldingPositions[WeaponPosDown].position, Quaternion.Euler(0, 0, 0));
            }
            else if (dir == Vector2.left)
            {
                Weapon.transform.SetPositionAndRotation(
                    WeaponHoldingPositions[WeaponPosLeft].position, Quaternion.Euler(0, 0, -90));
            }
            else
            {
                Weapon.transform.SetPositionAndRotation(
                    WeaponHoldingPositions[WeaponPosRight].position, Quaternion.Euler(0, 0, 90));
            }

            Weapon.Attack();
        }
    }
}
