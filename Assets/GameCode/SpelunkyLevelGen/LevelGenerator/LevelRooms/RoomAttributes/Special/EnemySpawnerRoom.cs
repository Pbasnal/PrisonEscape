using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes
{
    [CreateAssetMenu(fileName = "Enemy Spawn Room", menuName = "RoomAttributes/Special/Enemy Spawn Room", order = 51)]
    public class EnemySpawnerRoom : RoomAttribute<RoomSpecialityType>
    {
        public override void InvokeAttribute(GameObject gameObject)
        {
            if (gameObject.GetComponent<EnemySpawner>() != null)
            {
                return;
            }
            gameObject.AddComponent<EnemySpawner>();
        }

        public override bool IsRoomAttributePossible(int enterDirection, int exitDirection)
        {
            return false;
        }
    }
}
