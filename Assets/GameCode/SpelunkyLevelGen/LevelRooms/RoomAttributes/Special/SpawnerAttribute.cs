using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomScripts;

using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "Object Spawner", menuName = "RoomAttributes/Special/Spawner", order = 51)]
    public class SpawnerAttribute : RoomSpecialAttribute
    {
        public override void InvokeAttribute(GameObject gameObject)
        {
            if (gameObject.GetComponent<ObjectSpawner>() != null)
            {
                return;
            }
            gameObject.AddComponent<ObjectSpawner>();
        }

        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return true;
        }
    }
}
