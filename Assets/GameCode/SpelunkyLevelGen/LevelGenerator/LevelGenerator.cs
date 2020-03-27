using LayoutGenerator;
using Assets.Scripts.LevelRenderer;
using GameCode.Models;
using UnityEngine;

[RequireComponent(typeof(RoomCollection))]
[RequireComponent(typeof(LevelRenderer))]
public class LevelGenerator : MonoBehaviour
{
    //public SizeObject levelSize;
    public StartingRoom StartingRoom;
    
    public LevelData GenerateLevel(LevelData levelData)
    {
        levelData.SetStartingRoomCoordinates(new LevelCoordinate
        {
            Height = 0,
            Width = Random.Range(0, levelData.LevelSize.Width)
        });

        var levelRenderer = GetComponent<LevelRenderer>();
        var roomCollection = GetComponent<RoomCollection>();

        var layoutCreator = new FourByFourLayout(roomCollection.GetRoomTypes(), levelData.StartingRoomCoordinates);
        //new TwoByTwoLayout(roomCollection.GetRoomTypes(), levelData.StartingRoomCoordinates);
        //new FourByFourLayout(roomCollection.GetRoomTypes(), levelData.StartingRoomCoordinates);

        levelData.SetLevelLayout(layoutCreator.GenerateRoomLayout());
        levelData.SetRoomSize(roomCollection.RoomSize);
        levelData = levelRenderer.RenderBaseLevel(levelData, StartingRoom);

        return levelData;
    }
}