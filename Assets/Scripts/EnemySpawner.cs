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

    private Transform[] spawnPoints;

    [SerializeField] ObjectPooling enemyPool;

    private float minSpawnInterval = 1f;   // minimum spawn interval(starting spawn)
    private float maxSpawnInterval = 0.1f; // maximum spawn interval(Hardest spawn)
    private float currentSpawnInterval;

    private float timeLastSpawn;

    void Start()
    {
        spawnPoints = new Transform[] { spawn1, spawn2, spawn3, spawn4, spawn5, spawn6, spawn7, spawn8 };

        currentSpawnInterval = minSpawnInterval;
    }
    void SpawnEnemy()
    {

        int randomIndex = Random.Range(0, spawnPoints.Length); // random spawn point

        GameObject enemy = enemyPool.GetPooledObject(); // get enemy from pool

        if (enemy != null)
        {
            enemy.transform.position = spawnPoints[randomIndex].position; // set enemy position 

            enemy.SetActive(true);
        }
    }

    void Update()
    {

        timeLastSpawn += Time.deltaTime;

        // Check if it's time to spawn an enemy
        if (timeLastSpawn >= currentSpawnInterval)
        {
            SpawnEnemy();
            timeLastSpawn = 0f;
            Debug.Log(timeLastSpawn);
        }

        
        if (Time.time % 60f < Time.deltaTime) // Spawn time decreasess for every minute
        {
            Debug.Log("MINUTE");
            if (currentSpawnInterval > maxSpawnInterval)
            {
                currentSpawnInterval -= 0.1f;
                Debug.Log("CURRENT SPAWN INTERVAL: " + currentSpawnInterval);
            }
        }
    }
}
