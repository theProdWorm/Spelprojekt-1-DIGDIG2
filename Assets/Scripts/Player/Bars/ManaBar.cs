using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {
    public Sprite[ ] barStages;

    private Image img;
    private Player player;

    private int deltaMana;

    private void Start ( ) {
        player = FindObjectOfType<Player>( );

        img = GetComponent<Image>( );
    }

    private void Update ( ) {
        int mana = Mathf.FloorToInt(player.c_mana);

        if (mana != deltaMana)
            img.sprite = barStages[mana];
    }
}