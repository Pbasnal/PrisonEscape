﻿using GameCode.InteractionSystem.Mechanics;
using GameCode.InventorySystem;
using UnityEngine;

namespace GameCode.InteractionSystem.Reactions.ImmediateReactions
{
    public class PickUpItemReaction : Reaction
    {
        public Item Item;

        private StorageInventory _inventory;

        protected override void SpecificInit()
        {
            _inventory = FindObjectOfType<StorageInventory>();
        }

        public override void React(MonoBehaviour behaviour, Interactable interactable)
        {
            _inventory.AddItem(Item);

            if (interactable == null)
            {
                return;
            }
            Destroy(interactable.transform.gameObject);
        }

        protected override void ImmediateReaction()
        { }
    }
}
