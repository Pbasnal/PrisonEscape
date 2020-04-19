using GameCode.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode.Mechanics.InventorySystem
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] protected Image backgroundImage;
        [SerializeField] protected Image ItemImage;

        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_item == null)
                {
                    ItemImage.sprite = null;
                    ItemImage.enabled = false;
                }
                else
                {
                    ItemImage.sprite = _item.Sprite;
                    ItemImage.color = Color.white;
                    ItemImage.enabled = true;
                }
            }
        }

        private Item _item;

        protected void OnValidate()
        {
            if (ItemImage == null)
            {
                var itemImageObject = new GameObject();
                itemImageObject.name = "ItemImage";
                itemImageObject.transform.parent = transform;

                ItemImage = itemImageObject.AddComponent<Image>();
                ItemImage.enabled = false;
            }
        }

        public void SetBackground(Sprite bgSprite)
        {
            if (backgroundImage == null)
            {
                backgroundImage = GetComponent<Image>();
            }
            if (backgroundImage == null)
            {
                backgroundImage = gameObject.AddComponent<Image>();
            }

            backgroundImage.sprite = bgSprite;
        }
    }
}