using UnityEngine;

public class Tornado : MonoBehaviour, ISpell {
    public Spell spell;

    public float deviation;
    public float frequency;
    public float speed;

    private float angle = 0;

    private Vector2 direction;

    public Spell GetSpell ( ) => spell;

    private void Start ( ) {
        Direction facing = FindObjectOfType<Player>( ).deltaFacing;

        direction = Player.TranslateDirection(facing);
    }

    private void Update ( ) {
        angle += frequency * Time.deltaTime;

        if (angle > 2 * Mathf.PI)
            angle -= 2 * Mathf.PI;

        Vector2 perp = Vector2.Perpendicular(direction);

        Vector2 move = (direction * speed * Time.deltaTime) + (perp * deviation * Time.deltaTime * Mathf.Sin(angle));

        transform.position += (Vector3) move;
    }
}