using GameCode.InventorySystem;
using UnityEngine;

namespace GameCode.Mechanics.InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private EquipmentPanel equipmentPanel;

        private void Awake()
        {
            inventory.RegisterToAllItems(EquipFromInventory);
            equipmentPanel.RegisterToAllItems(UnequipFromInventory);
        }

        private void OnDestroy()
        {
            inventory.UnRegisterFromAllItems(EquipFromInventory);
            equipmentPanel.UnRegisterFromAllItems(UnequipFromInventory);
        }

        private void UnequipFromInventory(Item item)
        {
            if (item is EquippableItem)
            {
                Unequip((EquippableItem)item);
            }
        }

        private void EquipFromInventory(Item item)
        {
            if (item is EquippableItem)
            {
                Equip((EquippableItem) item);
            }
        }

        public void Equip(EquippableItem item)
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

        public void Unequip(EquippableItem item)
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
