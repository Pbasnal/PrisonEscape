using GameCode.InventorySystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private bool[] showItemSlots = new bool[Inventory.NumberOfItemSlots];
    private SerializedProperty itemImagesProperty;
    private SerializedProperty itemsProperty;
    private const string inventoryPropItemImagesName = "ItemImages";
    private const string inventoryPropItemsName = "Items";

    private GUIContent itemImageLabel = new GUIContent("Item Image");
    private GUIContent itemLabel = new GUIContent("Item Object");

    private void OnEnable()
    {
        itemImagesProperty = serializedObject.FindProperty(inventoryPropItemImagesName);
        itemsProperty = serializedObject.FindProperty(inventoryPropItemsName);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        for (int i = 0; i < Inventory.NumberOfItemSlots; i++)
        {
            ItemSlotGUI(i);
        }
        serializedObject.ApplyModifiedProperties();
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
}
