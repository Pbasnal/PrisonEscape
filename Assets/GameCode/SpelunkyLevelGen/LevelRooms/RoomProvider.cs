﻿using System;
using System.Collections.Generic;
using System.Linq;

using LockdownGames.GameCode.Models;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributes;
using LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomScripts;

using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms
{
    public class RoomProvider : MonoBehaviour
    {
        [Header("List of all possible rooms (RoomBuilder). Should be of same size.")]
        public List<RoomBuilder> rooms;
        public IntPair RoomSize => rooms[0].roomSize;

        [HideInInspector] public bool AllRoomsAdded = false;

        public RoomBuilder GetARoom(int enterDirection, int exitDirection)
        {
            var possibleRooms = rooms.Where(r => r.IsRoomPossible(enterDirection, exitDirection)).ToList();

            return possibleRooms[UnityEngine.Random.Range(0, possibleRooms.Count())];
        }

        public RoomBuilder GetARoom(List<RoomAttributeSO> roomAttributes)
        {
            var possibleRooms = rooms.Where(r => r.HasAllAttributes(roomAttributes)).ToList();
            if (possibleRooms == null || possibleRooms.Count == 0)
            {
                throw new Exception("No room is possible for the given set of attributes");
            }

            return possibleRooms[UnityEngine.Random.Range(0, possibleRooms.Count())];
        }

        public List<T> GetUniqueAttributesOfType<T>() where T : RoomAttributeSO
        {
            var uniqueAttributes = new HashSet<T>();

            foreach (var room in rooms)
            {
                var attribute = room.roomAttributes.FirstOrDefault(a => (a as T) != null);

                if (attribute == null)
                {
                    continue;
                }

                uniqueAttributes.Add(attribute as T);
            }

            return uniqueAttributes.ToList();
        }
    }
}
