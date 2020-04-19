using GameCode.InventorySystem;
using GameCode.Mechanics.InventorySystem;
using GameCode.Mechanics.InventorySystem.DataScripts;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private bool[] showItemSlots;//= new bool[Inventory.NumberOfItemSlots.Value];
    private SerializedProperty slotsInformationProperty;
    private SerializedProperty itemImagesProperty;
    private SerializedProperty itemsProperty;

    private const string slotsInformationName = "slotsInformation";
    private const string inventoryPropItemImagesName = "ItemImages";
    private const string inventoryPropItemsName = "Items";

    private GUIContent itemImageLabel = new GUIContent("Item Image");
    private GUIContent itemLabel = new GUIContent("Item Object");

    private void OnEnable()
    {
        var inventory = (Inventory)target;
        var itemsToShow = inventory.slotsInformation == null ? 0 : inventory.slotsInformation.NumberOfSlots;

        showItemSlots = new bool[itemsToShow];

        itemImagesProperty = serializedObject.FindProperty(inventoryPropItemImagesName);
        itemsProperty = serializedObject.FindProperty(inventoryPropItemsName);

        slotsInformationProperty = serializedObject.FindProperty(slotsInformationName);

        if (itemsToShow > 0)
        {
            AddOrRemoveItemSlots(inventory, inventory.slotsInformation);
        }
    }

    public override void OnInspectorGUI()
    {
        var inventory = (Inventory)target;
        var itemsToShow = inventory.slotsInformation == null ? 0 : inventory.slotsInformation.NumberOfSlots;

        var itemsOnUi = inventory.GetComponentsInChildren<ItemSlot>();

        if (itemsOnUi != null && itemsOnUi.Length != itemsToShow)
        {
            showItemSlots = new bool[itemsToShow];
            AddOrRemoveItemSlots(inventory, inventory.slotsInformation);
        }

        UpdateItemSlots(inventory, inventory.slotsInformation);

        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(slotsInformationProperty);

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        for (int i = 0; i < itemsToShow; i++)
        {
            ItemSlotGUI(i);
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void UpdateItemSlots(Inventory inventory, InventorySlotsInformation slotsInfo)
    {
        inventory.itemSlots = new ItemSlot[slotsInfo.NumberOfSlots];

        var itemSlotsFromUI = inventory.GetComponentsInChildren<ItemSlot>();
        if (itemSlotsFromUI != null)
        {
            return;
        }

        for (int i = 0; i < itemSlotsFromUI.Length; i++)
        {
            itemSlotsFromUI[i].SetBackground(slotsInfo.BackgroundImage);
        }
    }

    private void ItemSlotGUI(int index)
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        showItemSlots[index] = EditorGUILayout.Foldout(showItemSlots[index], "Item slot " + index);
        if (showItemSlots[index])
        {
            EditorGUILayout.PropertyField(itemImagesProperty.GetArrayElementAtIndex(index), itemImageLabel);
            EditorGUILayout.PropertyField(itemsProperty.GetArrayElementAtIndex(index), itemLabel);
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();
    }

    private void AddOrRemoveItemSlots(Inventory inventory, InventorySlotsInformation slotsInfo)
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
