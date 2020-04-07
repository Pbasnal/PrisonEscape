using UnityEngine;

[CreateAssetMenu(fileName = "IntPair", menuName = "LevelGen/IntPair", order = 51)]
public class IntPair : ScriptableObject
{
    public int x;
    public int y;

    public static IntPair CreatePair(int x, int y)
    {
        var intPair = ScriptableObject.CreateInstance<IntPair>();
        intPair.x = x;
        intPair.y = y;

        return intPair;
    }

    private IntPair()
    { }

    public void CopyFrom(IntPair pair)
    {
        x = pair.x;
        y = pair.y;
    }

    public IntPair Clone()
    {
        return new IntPair
        {
            x = x,
            y = y
        };
    }
}