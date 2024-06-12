using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemy;
    private Transform player;
    public int damageToPlayer;

    public float speed = 20f;
    private Vector2 move;

    public LayerMask whatIsGround;

    [SerializeField] private AudioSource rumblingSFX;
    public float damageInterval = 1.0f; // Interval in seconds between each damage application

    private bool isDamaging = false; // Flag to track if the enemy is currently damaging the player

    void Start()
    {
        // Find the player GameObject by tag and get its transform
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene. Make sure the player GameObject is tagged as 'Player'.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Set the destination of the NavMeshAgent to the player's position
            enemy.SetDestination(player.position);
        }

        // Toggle the rumbling sound effect based on the speed
        if (speed > 1)
        {
            rumblingSFX.enabled = true;
        }
        else
        {
            rumblingSFX.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isDamaging)
            {
                isDamaging = true;
                StartCoroutine(ApplyDamageOverTime(collision.gameObject));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDamaging = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator ApplyDamageOverTime(GameObject playerObject)
    {
        while (isDamaging)
        {
            Parameters parameters = new Parameters();
            parameters.PutExtra(PlayerStats.DAMAGE_VALUE, damageToPlayer);
            EventBroadcaster.Instance.PostEvent(EventNames.GameJam_Events.ON_DAMAGE, parameters);

            yield return new WaitForSeconds(damageInterval);
        }
    }
}
