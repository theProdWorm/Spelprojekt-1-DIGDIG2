using TMPro;
using UnityEngine;

public class Pablo : Entity {
    public TMP_Text spawnText;

    private float spawnTextTimer;

    protected override void Start ( ) {
        spawnText.enabled = true;

        spawnTextTimer = 10;
    }

    protected override void Update ( ) {
        if (spawnTextTimer > 0) {
            spawnTextTimer -= Time.deltaTime;

            if (spawnTextTimer <= 0) {
                spawnText.enabled = false;
            }
        }
    }
}