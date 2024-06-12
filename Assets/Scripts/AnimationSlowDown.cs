using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSlowDown : MonoBehaviour
{
    private Animator animator;
    public float slowDownFactor = 0.5f; // The factor by which to slow down the animation

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Check if the Animator component is found
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (animator != null)
        {
            // Adjust the Animator speed
            animator.speed = slowDownFactor;
        }
    }
}
