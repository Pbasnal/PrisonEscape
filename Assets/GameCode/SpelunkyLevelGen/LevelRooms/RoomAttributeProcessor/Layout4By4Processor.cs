using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelRooms.RoomAttributeProcessor
{
    [CreateAssetMenu(fileName = "4x4Layout", menuName = "AttributeProcessor/Connectedness/4x4Layout", order = 51)]
    public class Layout4By4Processor : LayoutProcessor
    {
        protected override int[,][] GetDirectionsMap()
        {
            /* 0 - Starting point
             * 1 - Left
             * 2 - Right
             * 3 - Up
             */

            // direction map => [enter direction, room location on width axis][possible exit directions]
            var directions = new int[4, 4][];

            directions[0, 0] = directions[3, 0] = new int[] { 2, 2, 3 };
            directions[0, 1] = directions[3, 1] = directions[0, 2] = directions[3, 2] = new int[] { 1, 1, 2, 2, 3 };
            directions[0, 3] = directions[3, 3] = new int[] { 1, 1, 3 };

            directions[1, 0] = new int[] { 3 };
            directions[1, 1] = directions[1, 2] = directions[0, 3];
            directions[1, 3] = new int[] { }; // this is not a possible situation

            directions[2, 0] = directions[1, 3]; // this is not a possible situation
            directions[2, 1] = directions[2, 2] = directions[0, 0];
            directions[2, 3] = directions[1, 0];

            return directions;
        }
    }
}
