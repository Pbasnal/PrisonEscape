using System;
using System.Collections.Generic;
using System.Linq;

using LockdownGames.Utilities;

using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomScripts
{
    public class ObjectSpawner : MonoBehaviour
    {
        public int TotalSpawnPoints => SpawnPositions == null ? 0 : SpawnPositions.Length;

        [SerializeField]
        private Transform[] SpawnPositions;

        public List<T> SpawnObjects<T>(GameObject obj, int amnt)
        {
            var objs = SpawnObject(obj, amnt);
            return objs.Select(o => o.GetComponent<T>()).ToList();
        }

        public T SpawnObject<T>(GameObject obj)
        {
            obj = SpawnObject(obj, 1)[0];
            return obj.GetComponent<T>();
        }

        public GameObject SpawnObject(GameObject obj)
        {
            obj = SpawnObject(obj, 1)[0];
            return obj;
        }

        public List<GameObject> SpawnObject(GameObject obj, int amnt)
        {
            if (SpawnPositions == null
                || SpawnPositions.Length == 0)
            {
                throw new Exception("No spawn points provided for the room");
            }

            var objs = new List<GameObject>();
            List<int> selected = new List<int>();
            for (int i = 0; i < amnt && i < TotalSpawnPoints; i++)
            {
                var selectedIndex =  Helper.RandomRangeWithoutRepeat(0, TotalSpawnPoints, selected);

                if (selectedIndex == -1)
                {
                    continue;
                }

                selected.Add(selectedIndex);
                objs.Add(Instantiate(obj, SpawnPositions[selectedIndex].position, Quaternion.identity, transform));
            }

            return objs;
        }


    }
}