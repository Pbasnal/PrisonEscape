using UnityEditor;

using UnityEngine;

namespace LockdownGames.EditorScripts.Utilities
{
    public static class Extensions
    {
        // Use this to remove the object at an index from an object array represented by a SerializedProperty.
        public static void RemoveFromObjectArrayAt(this SerializedProperty arrayProperty, int index)
        {
            // If the index is not appropriate or the serializedProperty this is being called from is not an array, throw an exception.
            if (index < 0)
                throw new UnityException("SerializedProperty " + arrayProperty.name + " cannot have negative elements removed.");

            if (!arrayProperty.isArray)
                throw new UnityException("SerializedProperty " + arrayProperty.name + " is not an array.");

            if (index > arrayProperty.arraySize - 1)
                throw new UnityException("SerializedProperty " + arrayProperty.name + " has only " + arrayProperty.arraySize + " elements so element " + index + " cannot be removed.");

            // Pull all the information from the target of the serializedObject.
            arrayProperty.serializedObject.Update();

            // If there is a non-null element at the index, null it.
            if (arrayProperty.GetArrayElementAtIndex(index).objectReferenceValue)
                arrayProperty.DeleteArrayElementAtIndex(index);

            // Delete the null element from the array at the index.
            arrayProperty.DeleteArrayElementAtIndex(index);

            // Push all the information on the serializedObject back to the target.
            arrayProperty.serializedObject.ApplyModifiedProperties();
        }

        public static void RemoveFromObjectArray<T>(this SerializedProperty arrayProperty, T elementToRemove)
            where T : Object
        {
            if (!arrayProperty.isArray)
            {
                throw new UnityException("Serialized Property " + arrayProperty.name + " is not an array.");
            }

            if (!elementToRemove)
            {
                throw new UnityException("Removing a null element is not support using this method.");
            }

            arrayProperty.serializedObject.Update();

            for (int i = 0; i < arrayProperty.arraySize; i++)
            {
                var elementProperty = arrayProperty.GetArrayElementAtIndex(i);

                if (elementProperty.objectReferenceValue == elementToRemove)
                {
                    arrayProperty.RemoveFromObjectArrayAt(i);
                    return;
                }
            }

            throw new UnityException("Element " + elementToRemove.name + " was not found in property " + arrayProperty.name);
        }

        public static void AddToObjectArray<T>(this SerializedProperty arrayProperty, T elementToAdd)
            where T : Object
        {
            if (!arrayProperty.isArray)
            {
                throw new UnityException("Serialized Property " + arrayProperty.name + " is not an array.");
            }

            if (!elementToAdd)
            {
                throw new UnityException("Adding a null element is not support using this method.");
            }

            arrayProperty.serializedObject.Update();

            arrayProperty.InsertArrayElementAtIndex(arrayProperty.arraySize);
            arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1).objectReferenceValue = elementToAdd;

            arrayProperty.serializedObject.ApplyModifiedProperties();
        }
    }
}