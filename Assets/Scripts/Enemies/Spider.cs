using UnityEngine;

public class Spider : Entity {
    public float jumpDistance;
    public float initialJumpCooldown;

    private float jumpCooldown;

    private void Update ( ) {
        if (jumpCooldown > 0) {
            jumpCooldown -= Time.deltaTime;
            return;
        }
    }

    private void Jump ( ) {
        // jump to the player and deal damage

        jumpCooldown = initialJumpCooldown;
    }
}