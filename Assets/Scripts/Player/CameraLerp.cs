using UnityEngine;

public class CameraLerp : MonoBehaviour {
    public Transform lerpPoint;
    public Transform player;
    public float mouseDistanceMod;
    public float speed;

    private void Update ( ) {
        Vector3 mousePos = Input.mousePosition;

        float angle = Mathf.Atan2(mousePos.x, mousePos.y);
        float mouseDistance = Vector3.Distance(player.position, Camera.current.ScreenToWorldPoint(mousePos)); // distance from player

        Vector3 mouseLerpPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

        Vector3 cursorLerp = Vector2.Lerp(
            transform.position,
            mouseLerpPos,
            mouseDistance * mouseDistanceMod * speed * Time.deltaTime);
        Vector3 dirLerp = Vector2.Lerp(
            transform.position,
            lerpPoint.position,
            speed * Time.deltaTime);

        var newPos = cursorLerp + dirLerp;
        newPos.z = -10;

        print("newPos" + newPos);

        //transform.position = newPos;
    }
}