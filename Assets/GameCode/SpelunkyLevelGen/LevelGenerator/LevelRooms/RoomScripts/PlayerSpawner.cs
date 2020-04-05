using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts
{
    public class PlayerSpawner : MonoBehaviour
    {
        public Transform PlayerSpwanLocation;

        public GameObject SpawnPlayer(GameObject player)
        {
            return Instantiate(player, PlayerSpwanLocation.position, Quaternion.identity);
        }
    }
}