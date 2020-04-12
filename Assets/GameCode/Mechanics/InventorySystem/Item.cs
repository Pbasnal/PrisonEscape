using UnityEngine;

namespace GameCode.InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items", order = 51)]
    public class Item : ScriptableObject
    {
        public Sprite Sprite;
        public GameObject GameObject;
    }
}
