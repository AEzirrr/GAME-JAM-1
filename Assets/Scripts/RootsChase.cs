using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsChase : MonoBehaviour
{
    public GameObject rootSegmentPrefab; 
    public Transform player;
    public float growSpeed;
    public float segmentLength;
    public float spawnInterval;
    public float stoppingDistance;
    public float maxYOffset;
    public float resumeDelay;

    public List<GameObject> roots;


    private Vector3 lastSegmentPosition;
    private Coroutine growCoroutine;
    private bool rootsReachedPlayer = false;

    void Start()
    {
        
        lastSegmentPosition = transform.position;
   
        growCoroutine = StartCoroutine(SpawnRootSegments());
    }

    IEnumerator SpawnRootSegments()
    {
        while (true)
        {
            if (!rootsReachedPlayer)
            {
                SpawnRootSegment();
                if (Vector3.Distance(lastSegmentPosition, player.position) <= stoppingDistance)
                {
                    rootsReachedPlayer = true;
                    yield return new WaitForSeconds(resumeDelay); // Delay before checking again
                }
            }
            else
            {
                // Check if the player has moved away from the stopping distance
                if (Vector3.Distance(lastSegmentPosition, player.position) > stoppingDistance)
                {
                    rootsReachedPlayer = false;
                }
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRootSegment()
    {
        
        Vector3 direction = (player.position - lastSegmentPosition).normalized;

        float randomYOffset = Random.Range(-maxYOffset, maxYOffset);
        Vector3 newSegmentPosition = lastSegmentPosition + direction * segmentLength + new Vector3(0, randomYOffset, 0);

        //Vector3 newSegmentPosition = lastSegmentPosition + direction * segmentLength;

        GameObject rootSegment = Instantiate(rootSegmentPrefab, newSegmentPosition, Quaternion.LookRotation(direction));

        float randomYRotation = Random.Range(70f, 90f);
        rootSegment.transform.Rotate(Vector3.up, randomYRotation);

        
        lastSegmentPosition = newSegmentPosition;

     
    }

    void Update()
    {
        //extra tings
    }
}
