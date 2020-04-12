using GameCode.InventorySystem;
using UnityEngine;

namespace GameCode.InteractionSystem.Reactions.ImmediateReactions
{
    public class PickUpItemReaction : Reaction
    {
        public Item Item;

        private Inventory _inventory;

        protected override void SpecificInit()
        {
            _inventory = FindObjectOfType<Inventory>();
        }

        public override void React(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);

            ImmediateReaction();
        }

        protected override void ImmediateReaction()
        {
            _inventory.AddItem(Item);
        }
    }
}
