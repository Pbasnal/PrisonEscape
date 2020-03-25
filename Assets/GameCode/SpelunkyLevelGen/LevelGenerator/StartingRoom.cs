using UnityEngine;

public class StartingRoom : RoomBuilder
{
    public Transform PlayerSpwanLocation;

    public GameObject SpawnPlayer(GameObject player)
    {
        return Instantiate(player, PlayerSpwanLocation.position, Quaternion.identity);
    }
}