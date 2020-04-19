using UnityEngine;

namespace GameCode.InventorySystem
{
    public enum EquipmentType
    {
        MeleeWeapon,
        ChestArmor,
        LegArmor,
        Shield
    }

    [CreateAssetMenu(fileName = "EquippableItem", menuName = "Inventory/EquippableItem", order = 51)]
    public class EquippableItem : Item
    {
        public EquipmentType equipmentType;
    }
}
