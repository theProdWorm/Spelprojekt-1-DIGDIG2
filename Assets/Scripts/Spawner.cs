using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[ ] enemyPrefabs;

    private Player player;

    private float spawnTimer;

    private void Start ( ) {
        player = FindObjectOfType<Player>( );
    }

    private void Update ( ) {
        int amountOfEnemies = (from entity in FindObjectsOfType<Entity>( )
                                 where !entity.CompareTag("Player")
                                 select entity).ToArray( ).Length;

        if (spawnTimer > 0) {
            spawnTimer -= Time.deltaTime;
            return;
        }

        if (amountOfEnemies >= 7) return;
        
        Vector3 spawnPoint = new Vector3(RandPolar( ), RandPolar( )) * 9;

        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], player.transform.position + spawnPoint, Quaternion.identity);

        spawnTimer = Random.Range(amountOfEnemies / 2f, amountOfEnemies);
    }

    private int RandPolar ( ) => Random.Range(1, 2) == 1 ? -1 : 1;
}