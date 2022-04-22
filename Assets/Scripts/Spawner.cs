using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[ ] enemyPrefabs;

    public float spawnDistance;

    private float spawnTimer;

    private Player player;

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

        float spawnAngle = Random.Range(0, 2 * Mathf.PI);
        Vector3 spawnPoint = new Vector3(Mathf.Cos(spawnAngle), Mathf.Sin(spawnAngle)) * spawnDistance;

        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], player.transform.position + spawnPoint, Quaternion.identity);

        spawnTimer = Random.Range(amountOfEnemies / 2f, amountOfEnemies);
    }
}