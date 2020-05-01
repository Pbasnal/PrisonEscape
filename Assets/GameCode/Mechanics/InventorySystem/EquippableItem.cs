using UnityEngine;

namespace LockdownGames.GameCode.Mechanics.InventorySystem
{
    public enum EquipmentType
    {
        MeleeWeapon,
        ChestArmor,
        LegArmor,
        Shield
    }

    [CreateAssetMenu(fileName = "Equippable Item", menuName = "Inventory/Equippable Item", order = 51)]
    public class EquippableItem : Item
    {
        public EquipmentType equipmentType;
        public Equippment EquippableObject;
    }
}
