using UnityEngine;

namespace LockdownGames.Mechanics.InventorySystem
{
    public abstract class Equippment : MonoBehaviour
    {
        public abstract void ActivateEquipmentOn(MonoBehaviour behaviour);
        public abstract void DeactivateEquipment();
    }

    public abstract class ActiveEquippment : Equippment
    {
        public abstract void UseActiveProperty(MonoBehaviour behaviour);
    }
}