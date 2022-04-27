using UnityEngine;

public class Steam : Spell {
    private Player player;

    void Start ( ) {
        player = FindObjectOfType<Player>( );
    }

    protected override void Update ( ) {
        base.Update( );

        transform.position = player.transform.position + (Vector3) LMTools.GetVector(player.deltaFacing);

        transform.rotation = LMTools.GetRotation(player.deltaFacing);
    }
}