using GameCode.InventorySystem;

namespace GameCode.InteractionSystem.Reactions.ImmediateReactions
{
    public class DropItemReaction : Reaction
    {
        public Item Item;               // Item to be removed from the Inventory.

        private Inventory _inventory;    // Reference to the Inventory component.

        protected override void SpecificInit()
        {
            _inventory = FindObjectOfType<Inventory>();
        }

        protected override void ImmediateReaction()
        {
            _inventory.RemoveItem(Item);
        }
    }
}
