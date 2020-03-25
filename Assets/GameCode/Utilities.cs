using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCode
{
    public class Utilities
    {
        public static int RandomRangeWithoutRepeat(int min, int max, List<int> alreadySelectedIndexes)
        {
            var remainingSelections = (max - min) - alreadySelectedIndexes.Count;

            for (int i = 0; i < remainingSelections; i++)
            {
                var selectedItemIndex = Random.Range(min, max);
                
                if (!alreadySelectedIndexes.Any(item => item == selectedItemIndex))
                {
                    return selectedItemIndex;
                }
            }

            return -1;
        }
    }
}
