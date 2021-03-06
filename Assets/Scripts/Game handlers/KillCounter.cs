using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour {
    public static int killCount = 0;

    public GameObject pablo;
    public float pablo_SD;

    public int pabloKillReq;

    private bool pabloSpawned;

    private TMP_Text tmp;

    private void Update ( ) {
        tmp = GetComponent<TMP_Text>( );

        tmp.text = "Kill count: " + (killCount == 69 ? "nice" : killCount.ToString( ));

        if (killCount >= pabloKillReq && !pabloSpawned)
            SpawnPablo( );
    }

    private void SpawnPablo ( ) {
        Instantiate(pablo, new Vector3(Random.Range(-pablo_SD, pablo_SD), Random.Range(pablo_SD, pablo_SD), 0), Quaternion.identity);

        pabloSpawned = true;
    }
}