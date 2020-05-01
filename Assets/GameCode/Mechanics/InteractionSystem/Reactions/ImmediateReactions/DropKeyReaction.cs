using LockdownGames.GameCode.Mechanics.InventorySystem;

namespace LockdownGames.GameCode.Mechanics.InteractionSystem.Mechanics.Reactions.ImmediateReactions
{
    public class DropKeyReaction : Reaction
    {
        public Item Item;               // Item to be removed from the Inventory.
        public Condition ConditionToReset;

        private StorageInventory _inventory;    // Reference to the Inventory component.

        protected override void SpecificInit()
        {
            _inventory = FindObjectOfType<StorageInventory>();
        }

        protected override void ImmediateReaction()
        {
            _inventory.RemoveItem(Item);

            foreach (var condition in AllConditions.Instance.Conditions)
            {
                if (condition.Hash != ConditionToReset.Hash)
                {
                    continue;
                }

                //Debug.Log("Setting key condition to false");
                condition.IsSatisfied = false;
            }
        }
    }
}
