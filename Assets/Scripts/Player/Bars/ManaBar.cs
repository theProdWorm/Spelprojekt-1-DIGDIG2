using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour {
    public Sprite[ ] barStages;

    private SpriteRenderer spriteRenderer;
    private Player player;

    private int deltaMana;

    private void Start ( ) {
        player = FindObjectOfType<Player>( );
    }

    private void Update ( ) {
        int mana = Mathf.FloorToInt(player.c_mana);

        if (mana != deltaMana)
            spriteRenderer.sprite = barStages[mana];
    }
}