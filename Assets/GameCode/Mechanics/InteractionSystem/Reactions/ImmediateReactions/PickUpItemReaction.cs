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

        public override void React(MonoBehaviour behaviour)
        {
            ImmediateReaction();
            Destroy(behaviour.transform.gameObject);
        }

        protected override void ImmediateReaction()
        {
            _inventory.AddItem(Item);
        }
    }
}
