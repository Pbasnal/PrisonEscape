using LockdownGames.GameCode.Player;

using UnityEditor;

using UnityEngine;

namespace LockdownGames.EditorScripts
{
    [CustomEditor(typeof(PlayerAi))]
    public class PlayerStateMachineDebugEditor : Editor
    {
        private void OnSceneGUI()
        {
            if (target != null)
            {
                var stateMachine = target as PlayerAi;
                var labelPos = new Vector2(stateMachine.transform.position.x, stateMachine.transform.position.y + 1);
                string state = stateMachine.currentState != null ? stateMachine.currentState.GetType().Name : "None";
                Handles.Label(labelPos, "State: " + state);
            }
        }
    }
}