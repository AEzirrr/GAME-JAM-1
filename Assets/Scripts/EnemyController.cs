using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;


    public float speed = 20f;
    private Vector2 move;


    public LayerMask whatIsGround;

    [SerializeField] private AudioSource rumblingSFX;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(Player.position);

        if(this.speed > 1)
        {
            rumblingSFX.enabled = true;
        }
        else
        {
            rumblingSFX.enabled = false;
        }
    }
}
