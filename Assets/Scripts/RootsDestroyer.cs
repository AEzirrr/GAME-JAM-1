using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Triggered: " + other.gameObject.name);

        if (other.CompareTag("Root"))
        {
            Debug.Log("Root detected: " + other.gameObject.name);
            DestroyRoot(other.gameObject);
        }
    }

    private void DestroyRoot(GameObject rootPart)
    {
        Transform rootParent = rootPart.transform.root;
        Debug.Log("Root Parent: " + rootParent.gameObject.name);
        Destroy(rootParent.gameObject);
        Debug.Log("Root Destroyed!");
    }
}
