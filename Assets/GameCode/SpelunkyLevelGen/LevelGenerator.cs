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
        /// <summary>
        /// Layout processor generates the basic layout of specified size using the connection attribute.
        /// </summary>
        [Header("Layout processor generates the basic layout using the connection attribute")]
        public LayoutProcessor layoutProcessor;

        public LevelData GenerateLevel(LevelData levelData)
        {
            var levelRenderer = GetComponent<BasicRenderer>();
            var roomProvider = GetComponent<RoomProvider>();

            levelData.RoomProvider = roomProvider;
            levelData.SetLevelSize(layoutProcessor.LevelSize);
            levelData.SetStartingRoomCoordinates(IntPair.CreatePair(0,
                UnityEngine.Random.Range(0, layoutProcessor.LevelSize.y)));

            levelData.SetLevelLayout(layoutProcessor.CreateLevelLayout(levelData));
            levelData.SetRoomSize(roomProvider.RoomSize);

            levelData = levelRenderer.RenderBaseLevel(levelData);

            return levelData;
        }

        public void ClearLevel()
        {
            while (transform.childCount != 0)
            {
                foreach (Transform child in transform)
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }
    }
}