using UnityEngine;

public class LevelAi : MonoBehaviour
{
    public GameObject player;

    private RoomBuilder[] rooms;
    private Size startingRoomIndex;


    public void SetLevelLayout(RoomBuilder[] rooms, Size startingRoomIndex)
    {
        this.rooms = rooms;
        this.startingRoomIndex = startingRoomIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
