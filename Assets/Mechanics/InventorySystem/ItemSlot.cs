using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LockdownGames.Mechanics.InventorySystem
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Item> OnItemClickEvent;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData == null || eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            if (Item == null || OnItemClickEvent == null)
            {
                return;
            }

            OnItemClickEvent(Item);
        }
    }
}