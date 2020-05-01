using System.Collections.Generic;
using System.Linq;

using LockdownGames.GameCode.Models;

using UnityEngine;

namespace LockdownGames.GameCode.GameAi
{
    public abstract class ALevelEnemiesPlacer : MonoBehaviour
    {
        public abstract LevelData PlaceEnemiesAsPerDifficulty(LevelData levelData, int numberOfRoomsToPlaceIn);

        protected bool IsRoomOnMainPath(int i, int j, List<IntPair> mainPath)
        {
            return mainPath.Any(coordinate => coordinate.x == i && coordinate.y == j);
        }
    }
}
