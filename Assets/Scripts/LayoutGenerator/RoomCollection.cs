using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.LayoutGenerator
{
    public class RoomCollection : MonoBehaviour
    {
        private RoomBuilder[] rooms;

        private IDictionary<IRoomType, List<RoomBuilder>> roomTypeMap;

        private void Start()
        {
            roomTypeMap = new Dictionary<IRoomType, List<RoomBuilder>>();

            foreach (var room in rooms)
            {
                if (!roomTypeMap.ContainsKey(room.roomType))
                {
                    roomTypeMap.Add(room.roomType, new List<RoomBuilder>());
                }
                roomTypeMap[room.roomType].Add(room);
            }
        }

        public List<IRoomType> GetRoomTypes()
        {
            return roomTypeMap.Keys.ToList();
        }

        public RoomBuilder[] GetRooms()
        {
            return rooms;
        }
    }
}
