using UnityEngine;

public class Arrow : MonoBehaviour {
    public float minDistance;
    public float offset;
    public float offsetBobSpeed;
    public float offsetBobMagnitude;

    [HideInInspector]
    public Transform pablo;

    private SpriteRenderer spriteRenderer;

    private float offsetBob;

    private void Start ( ) {
        spriteRenderer = GetComponent<SpriteRenderer>( );
    }

    private void Update ( ) {
        if (!pablo) return;

        Vector3 pabloPos = pablo.position;
        Vector3 playerPos = transform.parent.position;

        var dist = Vector3.Distance(pabloPos, playerPos);
        spriteRenderer.enabled = dist > minDistance;

        offsetBob += offsetBobSpeed * Time.deltaTime;
        if (offsetBob > Mathf.PI * 2)
            offsetBob -= Mathf.PI * 2;

        float _offset = offset + Mathf.Sin(offsetBob) * offsetBobMagnitude;
        
        var dirVector = pabloPos - playerPos;
        var angle = Mathf.Atan2(dirVector.y, dirVector.x);
        var dirPointer = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

        transform.localPosition = dirPointer * _offset;

        transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
    }
}