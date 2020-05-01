using LockdownGames.Mechanics.InventorySystem.DataScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LockdownGames.Mechanics.InventorySystem
{
    public class StorageInventory : MonoBehaviour
    {
        public InventorySlotsInformation slotsInformation;
        public ItemSlot[] itemSlots;

        [SerializeField] private List<Item> items;

        public void OnAnyItemClick(Action<Item> action)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnItemClickEvent += action;
            }
        }

        public void RemoveAllEventRegistrations(Action<Item> action)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnItemClickEvent -= action;
            }
        }

        public bool AddItem(Item itemToAdd)
        {
            for (int i = 0; i < slotsInformation.NumberOfSlots; i++)
            {
                if (itemSlots[i].Item != null)
                {
                    continue;
                }

                itemSlots[i].Item = itemToAdd;
                return true;
            }

            return false;
        }

        public bool RemoveItem(Item itemToRemove)
        {
            for (int i = 0; i < slotsInformation.NumberOfSlots; i++)
            {
                if (itemSlots[i].Item != itemToRemove)
                {
                    continue;
                }

                itemSlots[i].Item = null;
                
                return true;
            }

            return false;
        }

        // Assumption- Might not need it because Inventory has two separate methods
        // to add and remove items. Keeping this method in comments in case it's needed
        // in the future.
        //private void RefreshUi()
        //{
        //    int i = 0;
        //    for (;  i   < items.Count && i < itemSlots.Length; i++)
        //    {
        //        itemSlots[i].Item = items[i];
        //    }

        //    for (; i < itemSlots.Length; i++)
        //    {
        //        itemSlots[i].Item = null;
        //    }
        //}
    }
}
