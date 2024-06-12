using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawn1;
    [SerializeField] Transform spawn2;
    [SerializeField] Transform spawn3;
    [SerializeField] Transform spawn4;
    [SerializeField] Transform spawn5;
    [SerializeField] Transform spawn6;
    [SerializeField] Transform spawn7;
    [SerializeField] Transform spawn8;

    [SerializeField] ObjectPooling enemyPool;

    private Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the spawn points array
        spawnPoints = new Transform[] { spawn1, spawn2, spawn3, spawn4, spawn5, spawn6, spawn7, spawn8 };

    }

    void SpawnEnemy()
    {
        // Generate a random index to select a spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Get an enemy from the object pool
        GameObject enemy = enemyPool.GetPooledObject();

        // If an enemy was successfully retrieved from the pool
        if (enemy != null)
        {
            // Set the enemy's position to the selected spawn point
            enemy.transform.position = spawnPoints[randomIndex].position;

            // Activate the enemy
            enemy.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Example of spawning an enemy every 5 seconds
        // You can adjust the spawning logic as needed
        if (Time.time % .5f < Time.deltaTime)
        {
            SpawnEnemy();
        }
    }
}
