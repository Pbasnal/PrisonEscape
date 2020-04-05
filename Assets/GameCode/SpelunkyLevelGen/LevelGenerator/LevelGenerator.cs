using Assets.Scripts.LevelRenderer;
using GameCode.Models;
using SpelunkyLevelGen.LevelGenerator.LevelRooms;
using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributeProcessor;
using UnityEngine;

[RequireComponent(typeof(RoomProvider))]
[RequireComponent(typeof(LevelRenderer))]
public class LevelGenerator : MonoBehaviour
{
    public LayoutProcessor layoutCreator;
    //public GameObject Player;
    //public GameObject ExitDoor;

    //private void Start()
    //{
    //    var levelData = new LevelData();

    //    GenerateLevel(levelData);
    //}

    public LevelData GenerateLevel(LevelData levelData)
    {
        var levelRenderer = GetComponent<LevelRenderer>();
        var roomProvider = GetComponent<RoomProvider>();

        levelData.RoomProvider = roomProvider;
        levelData.SetLevelSize(layoutCreator.LevelSize);
        levelData.SetStartingRoomCoordinates(new LevelCoordinate
        {
            Height = 0,
            Width = UnityEngine.Random.Range(0, layoutCreator.LevelSize.Width)
        });

        // two by two has error as it does not update endposition in level layout
        // new TwoByTwoLayout(roomCollection.GetRoomTypes(), levelData.StartingRoomCoordinates);
        // new FourByFourLayout(roomCollection.GetRoomTypes(), levelData.StartingRoomCoordinates);

        levelData.SetLevelLayout(layoutCreator.CreateLevelLayout(levelData));
        levelData.SetRoomSize(roomProvider.RoomSize);

        levelData = levelRenderer.RenderBaseLevel(levelData);

        return levelData;
    }

        
}