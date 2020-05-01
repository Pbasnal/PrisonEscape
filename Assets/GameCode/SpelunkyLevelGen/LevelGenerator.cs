using LockdownGames.GameCode.Models;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRenderer;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributeProcessor;

using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen
{
    [RequireComponent(typeof(RoomProvider))]
    [RequireComponent(typeof(BasicRenderer))]
    public class LevelGenerator : MonoBehaviour
    {
        public LayoutProcessor layoutCreator;

        public LevelData GenerateLevel(LevelData levelData)
        {
            var levelRenderer = GetComponent<BasicRenderer>();
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
}