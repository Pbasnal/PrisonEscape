using System;
using UnityEngine;

namespace LockdownGames.Mechanics.InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private StorageInventory inventory;
        [SerializeField] private EquipmentsInventory equipmentPanel;

        public event Action<EquippableItem> OnItemEquipped;
        public event Action<EquippableItem> OnItemUnequipped;

        private void Awake()
        {
            inventory.OnAnyItemClick(EquipFromInventory);
            equipmentPanel.OnAnyItemClick(UnequipFromInventory);
        }

        private void OnDestroy()
        {
            inventory.RemoveAllEventRegistrations(EquipFromInventory);
            equipmentPanel.RemoveAllEventRegistrations(UnequipFromInventory);
        }

        private void UnequipFromInventory(Item item)
        {
            if (item is EquippableItem)
            {
                MoveItemToStorageInventory((EquippableItem)item);
                OnItemUnequipped?.Invoke((EquippableItem)item);
            }
        }

        private void EquipFromInventory(Item item)
        {
            if (item is EquippableItem)
            {
                MoveItemToEquippmentInventory((EquippableItem) item);
                OnItemEquipped?.Invoke((EquippableItem)item);
            }
        }

        public void MoveItemToEquippmentInventory(EquippableItem item)
        {
            if (!inventory.RemoveItem(item))
            {
                Debug.LogError("Unable to remove item from inventory - " + item.name);
                return;
            }

            if (equipmentPanel.AddItem(item, out var previousItem))
            {
                return;
            }
            
            Debug.LogError("Unable to add item from inventory - " + item.name);
            inventory.AddItem(item);
        }

        public void MoveItemToStorageInventory(EquippableItem item)
        {
            if (!inventory.AddItem(item))
            {
                return;
            }

            if (equipmentPanel.RemoveItem(item))
            {
                return;
            }

            inventory.AddItem(item);
        }
    }
}
