using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Ball;

public class PowerUPSpawner : MonoBehaviour
{




    [SerializeField] Transform spawn1;
    [SerializeField] Transform spawn2;
    [SerializeField] Transform spawn3;
    [SerializeField] Transform spawn4;

    private Transform[] spawnPoints;


    [SerializeField] ObjectPoolingPowerUp PowerUpPool;

    private List<Transform> occupiedSpawn = new List<Transform>();

    void Start()
    {
        spawnPoints = new Transform[] { spawn1, spawn2, spawn3, spawn4 };
    }
    void SpawnPowerUp()
    {
        
        if (occupiedSpawn.Count == spawnPoints.Length) // checking if all the spawn points are occupied
            return; // dont spawn


        int randomIndex = Random.Range(0, spawnPoints.Length);

        while (occupiedSpawn.Contains(spawnPoints[randomIndex])) // finds an unoccupied spawn point
        {
            randomIndex = Random.Range(0, spawnPoints.Length);
        }


        GameObject powerUp = PowerUpPool.GetPooledObject();

        if (powerUp != null)
        {
            powerUp.transform.position = spawnPoints[randomIndex].position;

            occupiedSpawn.Add(spawnPoints[randomIndex]);

            powerUp.SetActive(true);
        }
    }

    public void PowerUpPickedUp(Vector3 spawnPoint) // call to remove the spawn from the occupied positions
    {
        foreach (var spawn in spawnPoints)
        {
            if (spawn.position == spawnPoint)
            {
                occupiedSpawn.Remove(spawn);
                return; 
            }
        }
    }

    void Update() 
    {
        if (Time.time % 15f < Time.deltaTime) // spawn a powerup every 15 secs
        {
            SpawnPowerUp();
        }
    }
}
