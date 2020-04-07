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

    public LevelData GenerateLevel(LevelData levelData)
    {
        var levelRenderer = GetComponent<LevelRenderer>();
        var roomProvider = GetComponent<RoomProvider>();

        levelData.RoomProvider = roomProvider;
        levelData.SetLevelSize(layoutCreator.LevelSize);
        levelData.SetStartingRoomCoordinates(IntPair.CreatePair(0,
            UnityEngine.Random.Range(0, layoutCreator.LevelSize.y)));

        levelData.SetLevelLayout(layoutCreator.CreateLevelLayout(levelData));
        levelData.SetRoomSize(roomProvider.RoomSize);

        levelData = levelRenderer.RenderBaseLevel(levelData);

        return levelData;
    }

        
}