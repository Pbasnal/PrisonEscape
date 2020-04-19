using GameCode.Mechanics.InventorySystem.DataScripts;
using UnityEditor;
using UnityEngine;

namespace InventoryEditors
{
    [CustomEditor(typeof(InventorySlotsInformation))]
    public class InventorySlotInformationEditor : Editor
    {
        /*
         * public int NumberOfSlots;
         * public Sprite BackgroundImage;
         */

        private SerializedProperty numberOfSlotsProperty;
        private SerializedProperty backgroundImageProperty;

        private const string numberOfSlotsName = "NumberOfSlots";
        private const string backgroundImageName = "BackgroundImage";

        private void OnEnable()
        {
            numberOfSlotsProperty = serializedObject.FindProperty(numberOfSlotsName);
            backgroundImageProperty = serializedObject.FindProperty(backgroundImageName);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(numberOfSlotsProperty);
            EditorGUILayout.PropertyField(backgroundImageProperty);

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
