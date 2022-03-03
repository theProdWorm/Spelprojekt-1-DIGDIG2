using UnityEngine;

public class Spider : Entity {
    public float maxJumpDistance;
    public float jumpSpeedMod;
    public float initialJumpCooldown;

    public Spell jumpSpell;

    private float jumpCooldown;

    private Player player;

    protected override void Start ( ) {
        base.Start( );

        jumpCooldown = initialJumpCooldown;
        player = FindObjectOfType<Player>( );
    }

    protected override void Update ( ) {
        base.Update( );
        if (shouldReturn) return;

        if (jumpCooldown > 0) {
            jumpCooldown -= Time.deltaTime;
        }

        if (jumpCooldown <= 0) Jump( );
        else MoveEvasive( );
    }

    private void Jump ( ) {
        print("jump");
        
        var targetPos = player.transform.position;

        var distance = Vector2.Distance(targetPos, transform.position);
        if (distance > maxJumpDistance) return;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, jumpSpeedMod * c_speed * Time.deltaTime);

        // apply spell effects to player when jump is done
        if (transform.position == targetPos) {
            jumpCooldown = initialJumpCooldown;

            player.TakeHit(jumpSpell);
        }
    }

    private void MoveEvasive ( ) {
        var targetPos = player.transform.position;

        var distance = Vector2.Distance(targetPos, transform.position);

        Vector3 move = Vector3.zero;

        if (distance <= maxJumpDistance && jumpCooldown > 0) {
            move = GetDomAxis(transform.position - targetPos) * c_speed * Time.deltaTime;
        }

        transform.position += move;
    }
}