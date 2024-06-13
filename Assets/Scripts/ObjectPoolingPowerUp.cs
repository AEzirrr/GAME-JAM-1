using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolingPowerUp : MonoBehaviour
{
    public static ObjectPoolingPowerUp SharedInstance;
    public List<GameObject> pooledObjects;

    public GameObject medKit;
    public GameObject adrenaline;
    public GameObject scoreMultiplier;

    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        if (medKit== null)
        {
            return;
        }
        if (adrenaline == null)
        {
            return;
        }
        if (scoreMultiplier == null)
        {
            return;
        }

        pooledObjects = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(medKit);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(adrenaline);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(scoreMultiplier);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            int randomIndex = Random.Range(0, pooledObjects.Count);
            GameObject temp = pooledObjects[i];
            pooledObjects[i] = pooledObjects[randomIndex];
            pooledObjects[randomIndex] = temp;
        }

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy) // Checking if there are any inactive pooled object
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

}
