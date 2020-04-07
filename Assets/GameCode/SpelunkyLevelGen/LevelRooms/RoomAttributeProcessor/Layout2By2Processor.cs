using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelRooms.RoomAttributeProcessor
{
    [CreateAssetMenu(fileName = "2x2Layout", menuName = "AttributeProcessor/Connectedness/2x2Layout", order = 51)]
    public class Layout2By2Processor : LayoutProcessor
    {
        protected override int[,][] GetDirectionsMap()
        {
            /* 0 - Starting point
             * 1 - Left
             * 2 - Right
             * 3 - Up
             */

            // direction map => [enter direction, room location on width axis][possible exit directions]
            var directions = new int[4, 2][];

            directions[0, 0] = directions[3, 0] = new int[] { 2, 2, 3 };
            directions[0, 1] = directions[3, 1] = new int[] { 1, 1, 3 };

            directions[1, 0] = directions[2, 1] = new int[] { 3 };
            directions[1, 1] = directions[2, 0] = new int[0]; // not possible though

            return directions;
        }
    }
}
