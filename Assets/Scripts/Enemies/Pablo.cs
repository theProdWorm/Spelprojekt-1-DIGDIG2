using TMPro;
using UnityEngine;

public class Pablo : Entity {
    public TMP_Text spawnTextPrefab;

    private TMP_Text spawnText;

    private float spawnTextTimer;

    protected override void Start ( ) {
        base.Start( );

        var canvas = FindObjectOfType<Canvas>( );
        spawnText = Instantiate(spawnTextPrefab, canvas.transform);

        spawnText.enabled = true;

        spawnTextTimer = 10;
    }

    protected override void Update ( ) {
        base.Update( );

        if (spawnTextTimer > 0) {
            spawnTextTimer -= Time.deltaTime;

            if (spawnTextTimer <= 0) {
                spawnText.enabled = false;
            }
        }
    }
}