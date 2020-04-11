using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class Helper
    {
        public static int RandomRangeWithoutRepeat(int min, int max, List<int> alreadySelectedIndexes)
        {
            var remainingSelections = (max - min) - alreadySelectedIndexes.Count;

            for (int i = 0; i < max / 2; i++)
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
