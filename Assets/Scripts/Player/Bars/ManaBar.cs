using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour {
    public Sprite[ ] barStages;

    private Image img;
    private Player player;

    private int d_mana;

    private void Start ( ) {
        player = FindObjectOfType<Player>( );

        img = GetComponent<Image>( );
    }

    private void Update ( ) {
        int mana = Mathf.FloorToInt(player.c_mana);

        if (mana != d_mana)
            img.sprite = barStages[mana];

        d_mana = mana;
    }
}