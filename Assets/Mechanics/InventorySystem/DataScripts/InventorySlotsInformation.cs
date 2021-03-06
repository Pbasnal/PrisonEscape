﻿using UnityEngine;

namespace LockdownGames.Mechanics.InventorySystem.DataScripts
{
    [CreateAssetMenu(fileName = "InventorySlotsInformation", menuName = "Data/Int", order = 51)]
    public class InventorySlotsInformation : ScriptableObject
    {
        public int NumberOfSlots;
        public Sprite BackgroundImage;
    }
}
