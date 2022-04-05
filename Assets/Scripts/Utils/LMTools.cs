using UnityEngine;

public static class LMTools
{
    /// <summary>
    /// Returns an appropriate Vector2 according to the input Direction.
    /// </summary>
    public static Vector2 GetVector (Direction dir) => dir switch
    {
        Direction.left => Vector2.left,
        Direction.right => Vector2.right,
        Direction.down => Vector2.down,
        Direction.up => Vector2.up,
        _ => Vector2.zero
    };

    public static Vector2 DomAxis (Vector2 diagonal) {
        float angle = Mathf.Atan2(diagonal.y, diagonal.x);

        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        // return a new vector with only the dominant axis
        return Mathf.Abs(x) > Mathf.Abs(y) ? new Vector2(x, 0) : new Vector2(0, y);
    }
}