using System.Collections.Generic;
using UnityEngine;

namespace GameCode.GameAi.Code.Player
{
    public class WeaponHolder : MonoBehaviour
    {
        private IDictionary<string, Transform> WeaponHoldingPositions;

        private const string WeaponPositionTag = "";

        private const string WeaponPosDown = "WeaponPosDown";
        private const string WeaponPosUp = "WeaponPosUp";
        private const string WeaponPosLeft = "WeaponPosLeft";
        private const string WeaponPosRight = "WeaponPosRight";

        public void Awake()
        {
            WeaponHoldingPositions = new Dictionary<string, Transform>();

            foreach (Transform child in transform)
            {

            }

        }
    }
}
