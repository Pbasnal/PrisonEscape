using System.Collections.Generic;

using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes;

using UnityEngine;

namespace LockdownGames.GameCode.Models
{
    public class LevelLayout
    {
        public List<RoomAttributeSO>[,] AttributeLayout;
        public List<IntPair> MainPath;

        public Dictionary<RoomAttributeSO, List<IntPair>> AttributeToCoordinates;

        // these will be the final rendered rooms
        public GameObject[,] Rooms;

        public LevelLayout(IntPair levelSize)
        {
            MainPath = new List<IntPair>();
            AttributeLayout = new List<RoomAttributeSO>[levelSize.x, levelSize.y];
            Rooms = new GameObject[levelSize.x, levelSize.y];

            for (int i = 0; i < AttributeLayout.GetLength(0); i++)
            {
                for (int j = 0; j < AttributeLayout.GetLength(1); j++)
                {
                    AttributeLayout[i, j] = new List<RoomAttributeSO>();
                }
            }

            AttributeToCoordinates = new Dictionary<RoomAttributeSO, List<IntPair>>();
        }

        public void AddRoomAttribute(int height, int width, RoomAttributeSO roomAttribute)
        {
            if (!AttributeToCoordinates.ContainsKey(roomAttribute))
            {
                AttributeToCoordinates.Add(roomAttribute, new List<IntPair>());
            }

            AttributeToCoordinates[roomAttribute].Add(IntPair.CreatePair(height, width));
            AttributeLayout[height, width].Add(roomAttribute);
        }

        public List<T> GetRoomsWithAttribute<T>(RoomAttributeSO roomAttribute)
        {
            if (!AttributeToCoordinates.ContainsKey(roomAttribute))
            {
                return null;
            }

            var coordinates = AttributeToCoordinates[roomAttribute];

            var rooms = new List<T>();
            coordinates.ForEach(c =>
            {
                var script = Rooms[c.x, c.y].GetComponent<T>();
                if (script != null)
                {
                    rooms.Add(script);
                }
            });

            return rooms;
        }


        public void AddRenderedRoom(int height, int width, GameObject room)
        {
            Rooms[height, width] = room;
        }
    }
}


