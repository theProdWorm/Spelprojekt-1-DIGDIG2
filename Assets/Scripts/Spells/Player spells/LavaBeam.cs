using UnityEngine;

public class LavaBeam : Spell {
    [Tooltip("Mana drain interval in seconds.")]
    public float interval;

    private float timer;

    private Player player;

    private void Start ( ) {
        player = FindObjectOfType<Player>( );

        player.c_speed = 0;
    }

    protected override void Update ( ) {
        base.Update( );

        timer -= Time.deltaTime;

        if (timer <= 0) {
            timer += interval;

            player.c_mana -= manaCost;

            if (player.c_mana <= 0)
                Destroy(gameObject);
        }

        transform.position = player.transform.position + (Vector3) LMTools.GetVector(player.deltaFacing) * 3;

        transform.rotation = LMTools.GetRotation(player.deltaFacing);

        if (Player.KeyUp(player.castKeys))
            Destroy(gameObject);
    }

    private void OnDestroy ( ) {
        player.c_speed = player.i_speed;
    }
}