using GameCode;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomScripts
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
            for (int i = 0; i < amnt; i++)
            {
                var tr = SpawnPositions[Utilities.RandomRangeWithoutRepeat(0, TotalSpawnPoints, selected)];
                objs.Add(Instantiate(obj, tr.position, Quaternion.identity));
            }

            return objs;
        }


    }
}