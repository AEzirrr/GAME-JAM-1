using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePooling : MonoBehaviour
{
    public float spawnInterval = .001f; // Interval in seconds between spawns
    public float cubeLifetime = .4f;  // Time in seconds before a cube is despawned
    public Transform cubeSpawnPos;

    void Start()
    {
        // Start spawning cubes at regular intervals
        InvokeRepeating(nameof(SpawnCube), 0f, spawnInterval);
    }

    void SpawnCube()
    {
        GameObject cube = ObjectPooling.SharedInstance.GetPooledObject();
        if (cube != null)
        {
            cube.SetActive(true);
            cube.transform.position = cubeSpawnPos.position; 
            StartCoroutine(DespawnCube(cube, cubeLifetime));
        }
    }

    IEnumerator DespawnCube(GameObject cube, float delay)
    {
        yield return new WaitForSeconds(delay);
        ObjectPooling.SharedInstance.ReturnToPool(cube);
    }
}
