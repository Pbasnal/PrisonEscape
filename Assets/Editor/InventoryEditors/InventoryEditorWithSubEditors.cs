using LockdownGames.GameCode.Mechanics.InventorySystem;
using LockdownGames.GameCode.Mechanics.InventorySystem.DataScripts;

using UnityEditor;

using UnityEngine;

namespace LockdownGames.EditorScripts.InventoryEditors
{
    [CustomEditor(typeof(StorageInventory))]
    public class InventoryEditorWithSubEditors : EditorWithSubEditors<InventorySlotInformationEditor, InventorySlotsInformation>
    {
        public SerializedProperty slotsInformationProperty;

        private StorageInventory inventory;

        private const string slotsInformationName = "slotsInformation";

        private void OnEnable()
        {
            inventory = (StorageInventory)target;
            if (target == null)
            {
                DestroyImmediate(this);
                return;
            }

            slotsInformationProperty = serializedObject.FindProperty(slotsInformationName);

            CheckAndCreateSubEditor(inventory.slotsInformation);
        }

        protected override void SubEditorSetup(InventorySlotInformationEditor editor)
        { }

        public override void OnInspectorGUI()
        {
            var inventory = (StorageInventory)target;

            serializedObject.Update();

            CheckAndCreateSubEditor(inventory.slotsInformation);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(slotsInformationProperty);

            if (inventory.slotsInformation != null)
            {
                var itemsToShow = inventory.slotsInformation == null ? 0 : inventory.slotsInformation.NumberOfSlots;
                var itemsOnUi = inventory.GetComponentsInChildren<ItemSlot>();
                if (itemsOnUi != null && itemsOnUi.Length != itemsToShow)
                {
                    AddOrRemoveItemSlots(inventory, inventory.slotsInformation);
                }

                UpdateItemSlots(inventory, inventory.slotsInformation);

                EditorGUI.indentLevel++;
                for (int i = 0; i < subEditors.Length; i++)
                {
                    subEditors[i].OnInspectorGUI();
                }
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }

        private void UpdateItemSlots(StorageInventory inventory, InventorySlotsInformation slotsInfo)
        {
            var itemSlotsFromUI = inventory.GetComponentsInChildren<ItemSlot>();
            if (itemSlotsFromUI == null)
            {
                return;
            }

            for (int i = 0; i < itemSlotsFromUI.Length; i++)
            {
                itemSlotsFromUI[i].SetBackground(slotsInfo.BackgroundImage);
            }
        }

        private void AddOrRemoveItemSlots(StorageInventory inventory, InventorySlotsInformation slotsInfo)
        {
            inventory.itemSlots = new ItemSlot[slotsInfo.NumberOfSlots];
            var bgImage = slotsInfo.BackgroundImage;

            var numberOfSlotsOnUI = 0;
            var itemSlotsFromUI = inventory.GetComponentsInChildren<ItemSlot>();
            if (itemSlotsFromUI != null)
            {
                numberOfSlotsOnUI = itemSlotsFromUI.Length;
            }

            int i = 0;
            for (i = 0; i < inventory.itemSlots.Length && i < numberOfSlotsOnUI; i++)
            {
                inventory.itemSlots[i] = itemSlotsFromUI[i];
            }

            if (inventory.itemSlots.Length == numberOfSlotsOnUI)
            {
                return;
            }

            if (inventory.itemSlots.Length > numberOfSlotsOnUI)
            {
                // add new slots on UI
                for (; i < inventory.itemSlots.Length; i++)
                {
                    var gameObject = new GameObject();
                    gameObject.name = "ItemSlot " + i;
                    gameObject.AddComponent<RectTransform>();
                    gameObject.transform.parent = inventory.transform;

                    var itemSlot = gameObject.AddComponent<ItemSlot>();
                    itemSlot.SetBackground(bgImage);

                    inventory.itemSlots[i] = itemSlot;
                }

                return;
            }

            // extra slots are present on UI. Delete them
            for (; i < numberOfSlotsOnUI; i++)
            {
                DestroyImmediate(itemSlotsFromUI[i].gameObject);
            }
        }
    }
}
