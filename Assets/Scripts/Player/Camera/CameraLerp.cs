using UnityEngine;

public class CameraLerp : MonoBehaviour {
    public Transform lerpPoint;
    public float speed;
    public float offset;

    private void Update ( ) {
        Vector3 dirLerp = Vector2.Lerp(
            transform.position,
            lerpPoint.position,
            speed * Time.deltaTime);

        transform.position = new Vector3(dirLerp.x, dirLerp.y, -10);
    }
}