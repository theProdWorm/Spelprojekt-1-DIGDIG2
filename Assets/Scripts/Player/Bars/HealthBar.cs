using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Sprite[ ] barStages;

    private Image img;
    private Player player;

    private int d_hp;

    private void Start ( ) {
        player = FindObjectOfType<Player>( );

        img = GetComponent<Image>( );
    }

    private void Update ( ) {
        int hp = Mathf.FloorToInt(player.c_hp);

        if (hp != d_hp)
            img.sprite = barStages[hp];

        d_hp = hp;
    }
}