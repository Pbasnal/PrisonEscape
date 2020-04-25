using UnityEngine;

namespace GameCode.InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 51)]
    public class Item : ScriptableObject
    {
        // This sprite is shown on the ItemSlot of inventory
        public Sprite Sprite;
    }
}
