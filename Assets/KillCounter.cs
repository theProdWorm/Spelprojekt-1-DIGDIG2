using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour {
    public static int killCount = 0;

    public GameObject pablo;

    private bool pabloSpawned;

    private TMP_Text tmp;

    private void Update ( ) {
        tmp = GetComponent<TMP_Text>( );

        tmp.text = "Kill count: " + killCount;

        if (killCount >= 5 && !pabloSpawned)
            SpawnPablo( );
    }

    private void SpawnPablo ( ) {
        Instantiate(pablo, new Vector3(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f), 0), Quaternion.identity);

        pabloSpawned = true;
    }
}