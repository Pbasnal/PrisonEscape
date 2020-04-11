using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ZombieAi))]
public class ZombieDebugTextEditor : Editor
{
    private void OnSceneGUI()
    {
        // get the chosen game object
        if (target != null)
        {
            var zombie = target as ZombieAi;

            //Debug.Log("Creating handles");

            var labelPos = new Vector2(zombie.transform.position.x, zombie.transform.position.y + 1);

            string state = zombie.State != null ? zombie.State.GetType().Name : "None";

            Handles.Label(labelPos, "State: " + state);
        }
    }
}