using SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace LayoutGenerator
{
    public class LevelLayout
    {
        public List<RoomAttributeSO>[,] AttributeLayout;
        public List<LevelCoordinate> MainPath;

        public Dictionary<RoomAttributeSO, List<ISize>> AttributeToCoordinates;

        // these will be the final rendered rooms
        public GameObject[,] Rooms;

        public LevelLayout(ISize levelSize)
        {
            MainPath = new List<LevelCoordinate>();
            AttributeLayout = new List<RoomAttributeSO>[levelSize.Height, levelSize.Width];
            Rooms = new GameObject[levelSize.Height, levelSize.Width];

            for (int i = 0; i < AttributeLayout.GetLength(0); i++)
            {
                for (int j = 0; j < AttributeLayout.GetLength(1); j++)
                {
                    AttributeLayout[i, j] = new List<RoomAttributeSO>();
                }
            }

            AttributeToCoordinates = new Dictionary<RoomAttributeSO, List<ISize>>();
        }

        public void AddRoomAttribute(int height, int width, RoomAttributeSO roomAttribute)
        {
            if (!AttributeToCoordinates.ContainsKey(roomAttribute))
            {
                AttributeToCoordinates.Add(roomAttribute, new List<ISize>());
            }

            AttributeToCoordinates[roomAttribute].Add(new Size { Height = height, Width = width });
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
                var script = Rooms[c.Height, c.Width].GetComponent<T>();
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


