﻿using System.Collections.Generic;

using LockdownGames.Mechanics.InventorySystem;

using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics
{
    public class EquippedMechanics : MonoBehaviour
    {
        private InventoryManager inventoryManager;

        private List<ActiveEquippment> activeEquipments;
        private List<Equippment> passiveEquippments;

        private void Awake()
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager == null)
            {
                throw new UnityException("Add an inventory maanger before starting the game");
            }
            inventoryManager.OnItemEquipped += EquipItem;
            inventoryManager.OnItemUnequipped += UnequipItem;

            activeEquipments = new List<ActiveEquippment>();
            passiveEquippments = new List<Equippment>();
        }

        private void OnDestroy()
        {
            inventoryManager.OnItemEquipped -= EquipItem;
            inventoryManager.OnItemUnequipped -= UnequipItem;
        }

        //private void Update()
        //{
        //    // todo: how to activate properties?
        //}

        private void EquipItem(EquippableItem item)
        {
            if (item.EquippableObject == null)
            {
                Debug.LogError("No gameobject has been set for " + item.name);
                return;
            }

            if (item.EquippableObject is ActiveEquippment)
            {
                activeEquipments.Add(item.EquippableObject as ActiveEquippment);
            }
            else
            {
                passiveEquippments.Add(item.EquippableObject as Equippment);
            }

            item.EquippableObject.ActivateEquipmentOn(this);
        }

        private void UnequipItem(EquippableItem item)
        {
            if (item.EquippableObject == null)
            {
                Debug.LogError("No gameobject has been set for " + item.name);
                return;
            }

            if (item.EquippableObject is ActiveEquippment)
            {
                activeEquipments.Remove(item.EquippableObject as ActiveEquippment);
            }
            else
            {
                passiveEquippments.Remove(item.EquippableObject as Equippment);
            }

            item.EquippableObject.DeactivateEquipment();
        }
    }
}
