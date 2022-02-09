using UnityEngine;

public class LerpPoint : MonoBehaviour {
    public Player player;
    public CameraLerp cam;

    private void Update ( ) {
        if (player.facing != Direction.none) {
            transform.position = player.transform.position +
                (Vector3) Player.TranslateDirection(player.facing) * cam.offset;
        }
    }
}