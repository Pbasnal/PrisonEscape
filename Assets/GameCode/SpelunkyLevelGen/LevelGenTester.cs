using EasyButtons;

using LockdownGames.GameCode.Models;

using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen
{
    public class LevelGenTester : MonoBehaviour
    {
        public LevelGenerator levelGenerator;

        [Button]
        public void GenerateRandomLevel()
        {
            var levelData = new LevelData();
            levelGenerator.ClearLevel();
            levelGenerator.GenerateLevel(levelData);
        }

        [Button]
        public void ClearLevel()
        {
            levelGenerator.ClearLevel();
        }
    }
}
