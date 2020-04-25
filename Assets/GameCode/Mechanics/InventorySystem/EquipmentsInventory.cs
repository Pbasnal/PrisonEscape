using GameCode.InventorySystem;
using System;
using UnityEngine;

namespace GameCode.Mechanics.InventorySystem
{
    public class EquipmentsInventory : MonoBehaviour
    {
        public EquipmentSlot[] itemSlots;

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

        public bool AddItem(EquippableItem itemToAdd, out EquippableItem previousItem)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].equipmentType != itemToAdd.equipmentType)
                {
                    continue;
                }

                previousItem = (EquippableItem)itemSlots[i].Item;
                itemSlots[i].Item = itemToAdd;

                return true;
            }

            previousItem = null;
            return false;
        }

        public bool RemoveItem(EquippableItem itemToRemove)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].equipmentType != itemToRemove.equipmentType)
                {
                    continue;
                }

                itemSlots[i].Item = null;

                return true;
            }

            return false;
        }
    }
}
