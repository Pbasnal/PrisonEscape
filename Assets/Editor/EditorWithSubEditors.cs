using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LockdownGames.EditorScripts
{
    public abstract class EditorWithSubEditors<TEditor, TTarget> : Editor
        where TEditor : Editor
        where TTarget : Object
    {
        protected TEditor[] subEditors;

        protected void CheckAndCreateSubEditors(TTarget[] subEditorTargets)
        {
            CleanupEmptyEditors();
            if (subEditors != null && subEditors.Length == subEditorTargets.Length)
            {
                return;
            }

            CleanupEditors();

            subEditors = new TEditor[subEditorTargets.Length];
            for (int i = 0; i < subEditors.Length; i++)
            {
                if (subEditorTargets[i] == null)
                {
                    continue;
                }
                subEditors[i] = CreateEditor(subEditorTargets[i]) as TEditor;
                SubEditorSetup(subEditors[i]);
            }

            //CleanupEmptyEditors();
        }

        protected void CheckAndCreateSubEditor(TTarget subEditorTarget)
        {
            if (subEditors != null || subEditorTarget == null)
            {
                return;
            }

            CleanupEditors();

            subEditors = new TEditor[1];
            subEditors[0] = CreateEditor(subEditorTarget) as TEditor;
            SubEditorSetup(subEditors[0]);
        }

        protected void CleanupEmptyEditors()
        {
            if (subEditors == null)
            {
                return;
            }

            var nonEmptyEditors = new List<TEditor>();
            for (int i = 0; i < subEditors.Length; i++)
            {
                if (subEditors[i] == null)
                {
                    continue;
                }
                nonEmptyEditors.Add(subEditors[i]);
            }

            subEditors = nonEmptyEditors.ToArray();
        }

        protected void CleanupEditors()
        {
            if (subEditors == null)
            {
                return;
            }

            for (int i = 0; i < subEditors.Length; i++)
            {
                DestroyImmediate(subEditors[i]);
            }

            subEditors = null;
        }

        protected abstract void SubEditorSetup(TEditor editor);
    }
}