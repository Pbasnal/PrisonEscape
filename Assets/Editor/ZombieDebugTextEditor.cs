using LockdownGames.GameAi.Enemies.Zombies;

using UnityEditor;

using UnityEngine;

namespace LockdownGames.EditorScripts
{
    [CustomEditor(typeof(ZombieAi))]
    public class ZombieDebugTextEditor : Editor
    {
        private void OnSceneGUI()
        {
            if (target != null)
            {
                var zombie = target as ZombieAi;
                var labelPos = new Vector2(zombie.transform.position.x, zombie.transform.position.y + 1);
                string state = zombie.currentState != null ? zombie.currentState.GetType().Name : "None";
                Handles.Label(labelPos, "State: " + state);
            }
        }
    }
}