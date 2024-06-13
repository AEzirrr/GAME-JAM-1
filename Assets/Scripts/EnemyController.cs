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
    public float damageInterval = 0.01f; 

    private bool isDamaging = false; 

    void Start()
    {
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
            enemy.SetDestination(player.position);
        }

        if (speed > 1)
        {
            rumblingSFX.enabled = true;
        }
        else
        {
            rumblingSFX.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that triggered this event is the player's slash effect
        if (other.gameObject.CompareTag("SlashEffect"))
        {
            StopAllCoroutines();
            this.gameObject.SetActive(false);
            Parameters parameters = new Parameters();
            EventBroadcaster.Instance.PostEvent(EventNames.GameJam_Events.ADD_SCORE, parameters);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDamaging = true;
            StartCoroutine(ApplyDamageOverTime(collision.gameObject));
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
