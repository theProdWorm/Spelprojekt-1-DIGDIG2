using UnityEngine;

public class Spider : Entity {
    public float maxJumpDistance;
    public float jumpSpeedMod;
    public float initialJumpCooldown;

    public GameObject jumpSpell;

    private float jumpCooldown;
    private bool jumping;

    private Player player;

    private Vector3 targetPos;
    private float distance;
    private int moveDir = 1;

    protected override void Start ( ) {
        base.Start( );

        jumpCooldown = initialJumpCooldown;
        player = FindObjectOfType<Player>( );
    }

    protected override void Update ( ) {
        base.Update( );
        if (stunned) {
            transform.rotation = LMTools.GetRotation(Direction.left);
            return;
        }
        else {
            transform.rotation = LMTools.GetRotation(Direction.none);
        }

        if (jumpCooldown > 0) {
            jumpCooldown -= Time.deltaTime;
        }

        if (!jumping) targetPos = player.transform.position;
        distance = Vector2.Distance(targetPos, transform.position);

        if (jumpCooldown <= 0) {
            if (distance <= maxJumpDistance) Jump( );
            else CloseDistance( );
        }
        else Juke( );
    }

    private void Jump ( ) {
        jumping = true;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, jumpSpeedMod * c_speed * Time.deltaTime);

        // apply spell effects to player when jump is done
        if (transform.position == targetPos) {
            jumpCooldown = initialJumpCooldown;
            jumping = false;

            CastSpell(jumpSpell);
        }
    }

    private void Juke ( ) {
        var targetPos = player.transform.position;

        var distance = Vector2.Distance(targetPos, transform.position);

        Vector3 move;

        var oppDir = LMTools.DomAxis(transform.position - targetPos);

        if (distance <= maxJumpDistance && jumpCooldown > 0) {
            move = oppDir * c_speed * Time.deltaTime;
        }
        else {
            bool switchDir = Mathf.Round(Random.Range(0, 1 / Time.deltaTime)) == 1; // 1% chance of switching direction each frame
            if (switchDir) SwitchDir( );

            var perp = Vector2.Perpendicular(oppDir);

            move = perp * moveDir * c_speed * Time.deltaTime;
        }

        transform.position += move;
    }

    private void SwitchDir ( ) => moveDir = -moveDir;

    private void CloseDistance ( ) {
        Vector2 towardPlayer = LMTools.DomAxis(targetPos - transform.position);

        Vector3 move = towardPlayer * c_speed * Time.deltaTime;

        transform.position += move;
    }
}