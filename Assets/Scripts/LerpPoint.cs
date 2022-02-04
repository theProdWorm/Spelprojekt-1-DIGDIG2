using UnityEngine;

public class LerpPoint : MonoBehaviour {
    public float offset;
    public Player player;

    private void Update ( ) {
        transform.position = player.transform.position +
            (Vector3) Player.TranslateDirection(player.facing) * offset;
    }
}