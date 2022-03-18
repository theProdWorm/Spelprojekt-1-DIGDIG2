using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
    public KeyCode[ ] leftKeys;
    public KeyCode[ ] rightKeys;
    public KeyCode[ ] downKeys;
    public KeyCode[ ] upKeys;
    private KeyCode[ ][ ] directionKeys;
    private List<Direction> inputDirs;

    [HideInInspector]
    public Direction facing;

    private List<Element> selectedElements = new List<Element>( );

    protected override void Start ( ) {
        base.Start( );

        inputDirs = new List<Direction>( );

        directionKeys = new KeyCode[ ][ ] { leftKeys, rightKeys, downKeys, upKeys };
    }

    protected override void Update ( ) {
        base.Update( );

        reanimator.Set("player_root", 0);

        Move( );
    }

    private void Move ( ) {
        GetMoveInput( );

        facing = inputDirs.Count != 0 ? inputDirs[^1] : Direction.none;

        Vector3 _move = TranslateDirection(facing) * c_speed * Time.deltaTime;

        transform.position += _move;

        if (facing != Direction.none)
            reanimator.Set("player_movement", (int) facing);
    }

    /// <summary>
    /// Adds or removes Direction elements from the inputDirs list according to input.
    /// </summary>
    private void GetMoveInput ( ) {
        for (int i = 0; i < directionKeys.Length; i++) {
            if (KeyDown(directionKeys[i])) {
                inputDirs.Add((Direction) i);
            }
            else if (KeyUp(directionKeys[i]) && inputDirs.Contains((Direction) i)) {
                inputDirs.RemoveAll(x => x == (Direction) i);
            }
        }
    }

    private bool KeyDown (KeyCode[ ] keyArray) {
        for (int i = 0; i < keyArray.Length; i++) {
            if (Input.GetKeyDown(keyArray[i]))
                return true;
        }
        return false;
    }

    private bool KeyUp (KeyCode[ ] keyArray) {
        for (int i = 0; i < keyArray.Length; i++) {
            if (Input.GetKeyUp(keyArray[i]))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Returns an appropriate Vector2 according to the input Direction.
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static Vector2 TranslateDirection (Direction dir) => dir switch {
        Direction.left => Vector2.left,
        Direction.right => Vector2.right,
        Direction.down => Vector2.down,
        Direction.up => Vector2.up,
        _ => Vector2.zero
    };
}