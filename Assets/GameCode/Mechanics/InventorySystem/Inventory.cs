using UnityEngine;
using UnityEngine.UI;

namespace GameCode.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public const int NumberOfItemSlots = 4;

        public Image[] ItemImages = new Image[NumberOfItemSlots];
        public Item[] Items = new Item[NumberOfItemSlots];

        public void AddItem(Item itemToAdd)
        {
            for (int i = 0; i < NumberOfItemSlots; i++)
            {
                if (Items[i] != null)
                {
                    continue;
                }

                Items[i] = itemToAdd;
                ItemImages[i].sprite = itemToAdd.Sprite;
                ItemImages[i].enabled = true;
                break;
            }
        }

        public void RemoveItem(Item itemToRemove)
        {
            for (int i = 0; i < NumberOfItemSlots; i++)
            {
                if (Items[i] != itemToRemove)
                {
                    continue;
                }

                Items[i] = null;
                ItemImages[i].sprite = null;
                ItemImages[i].enabled = false;
                break;
            }
        }
    }
}
