using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "Starting Room", menuName = "RoomAttributes/Special/Starting Room", order = 51)]
    public class StartingRoom : RoomAttribute<RoomSpecialityType>
    {
        public override void InvokeAttribute(GameObject gameObject)
        {
            if (gameObject.GetComponent<PlayerSpawner>() != null)
            {
                return;
            }
            gameObject.AddComponent<PlayerSpawner>();
        }

        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
