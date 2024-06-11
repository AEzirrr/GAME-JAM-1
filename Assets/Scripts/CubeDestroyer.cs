using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that triggered this event is the player's slash effect
        if (other.gameObject.CompareTag("SlashEffect"))
        {
            DestroyCube();
        }
    }

    private void DestroyCube()
    {
        Destroy(gameObject);
    }
}
