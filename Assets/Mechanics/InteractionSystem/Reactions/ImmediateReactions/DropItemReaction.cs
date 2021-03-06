﻿using LockdownGames.Mechanics.InventorySystem;

namespace LockdownGames.Mechanics.InteractionSystem.Reactions.ImmediateReactions
{
    public class DropItemReaction : Reaction
    {
        public Item Item;               // Item to be removed from the Inventory.

        private StorageInventory _inventory;    // Reference to the Inventory component.

        protected override void SpecificInit()
        {
            _inventory = FindObjectOfType<StorageInventory>();
        }

        protected override void ImmediateReaction()
        {
            _inventory.RemoveItem(Item);
        }
    }
}
