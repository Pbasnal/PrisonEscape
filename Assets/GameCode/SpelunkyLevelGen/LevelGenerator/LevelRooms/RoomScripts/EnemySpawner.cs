using GameCode.GameAi;
using System.Collections.Generic;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform[] EnemySpawnPositions;
        public EnemyCollection EnemyCollection;

        public List<ZombieAi> SpawnEnemy()
        {
            var enemies = new List<ZombieAi>();

            if (EnemySpawnPositions == null
                || EnemySpawnPositions.Length == 0
                || EnemyCollection == null)
            {
                return enemies;
            }

            foreach (var tr in EnemySpawnPositions)
            {
                enemies.Add(Instantiate(EnemyCollection.GetAnEnemy(), tr.position, Quaternion.identity));
            }

            return enemies;
        }
    }
}