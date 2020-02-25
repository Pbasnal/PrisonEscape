using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.LayoutGenerator
{
    public class RoomCollection : MonoBehaviour
    {
        public RoomBuilder[] rooms;

        public SizeObject RoomSize => rooms[0].roomSize;

        private IDictionary<IRoomType, List<RoomBuilder>> roomTypeMap;

        private void Awake()
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

        public RoomBuilder GetARoom(IRoomType roomType)
        {
            if (!roomTypeMap.ContainsKey(roomType))
            {
                return null;
            }

            var selectedRooms = roomTypeMap[roomType];
            return selectedRooms[UnityEngine.Random.Range(0, selectedRooms.Count)];
        }
    }
}
