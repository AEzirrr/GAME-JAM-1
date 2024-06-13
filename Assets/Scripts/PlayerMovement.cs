using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Model")]
    [SerializeField] private Transform playerModel;

    [Header("Movement")]
    public float moveSpeed;
    public float waterMoveSpeed;
    public float rotationSpeed;

    public float groundDrag;

    [Header("Attack")]
    public float attackCooldown = 1f; // Duration of attack cooldown
    private bool readyToAttack = true; // Whether the player can attack
    private float nextAttackTime = 0f; // Next time the player can attack

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode slashKey = KeyCode.J;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    public LayerMask water;
    public LayerMask enemy;
    bool grounded;

    public Transform orientation;
    public Transform slashPosition;

    [Header("Raycast")]
    public Transform raycastOrigin; // Custom raycast origin

    [Header("Attack")]
    public GameObject slashEffect;

    [SerializeField] private AudioClip slashSFX;
    [SerializeField] private AudioSource runningSFX;
    [SerializeField] private AudioClip jumpSFX;

    private float defaultMoveSpeed;


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private Animator playerAnimation;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true; // Ensure readyToJump is true at start
        playerAnimation = GetComponent<Animator>();

        defaultMoveSpeed = moveSpeed;

        if (slashEffect == null)
        {
            Debug.LogError("Slash effect game object not assigned in " + gameObject.name);
        }
        else
        {
            slashEffect.SetActive(false); // Ensure the slash effect is initially inactive
        }
    }

    private void Update()
    {
        // Use custom raycast origin if set, otherwise default to player's position
        Vector3 rayOrigin = raycastOrigin ? raycastOrigin.position : transform.position;
        grounded = Physics.Raycast(rayOrigin, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround | enemy);
        bool inWater = Physics.Raycast(rayOrigin, Vector3.down, playerHeight * 0.5f + 0.2f, water);

        // Debugging ground detection
        Debug.DrawRay(rayOrigin, Vector3.down * (playerHeight * 0.5f + 0.2f), grounded ? Color.green : Color.red);
        //Debug.Log("Grounded: " + grounded);
        //Debug.Log("Ready to Jump: " + readyToJump);

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && grounded)
        {
            runningSFX.enabled = true;
        }
        else
        {
            runningSFX.enabled = false;
        }
  

        MyInput();

        SpeedControl();

        bool isMoving = moveDirection.magnitude > 0.1f;

        if (isMoving)
        {
            if (playerAnimation != null)
            {
                playerAnimation.SetBool("isRunning", true);
                playerAnimation.SetBool("Jumped", false);
                //Debug.Log("Run animation triggered!");
            }
        }
        else
        {
            if (playerAnimation != null)
            {
                playerAnimation.SetBool("isRunning", false);
                playerAnimation.SetBool("Jumped", false);
                //Debug.Log("Idle animation triggered!");
            }
        }

        if (slashEffect != null && playerModel != null)
        {
            slashEffect.transform.rotation = playerModel.rotation;
        }

        if (Input.GetKeyDown(slashKey) && readyToAttack)
        {
            ActivateSlashEffect();
            SFXManager.instance.PlaySfxClip(slashSFX, transform, .5f);
        }

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            SFXManager.instance.PlaySfxClip(jumpSFX, transform, .1f);
            playerAnimation.SetBool("Jumped", true);
            Invoke(nameof(ResetJump), jumpCooldown);

        }

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (inWater)
        {
            defaultMoveSpeed = waterMoveSpeed; // Adjust move speed in water
        }
        else
        {
            defaultMoveSpeed = moveSpeed; // Reset move speed when out of water
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            playerModel.forward = Vector3.Slerp(playerModel.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

    
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * defaultMoveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * defaultMoveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > defaultMoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * defaultMoveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ActivateSlashEffect()
{
    if (slashEffect != null && orientation != null && slashPosition != null)
    {
        slashEffect.transform.position = slashPosition.position;
        slashEffect.transform.rotation = Quaternion.LookRotation(orientation.forward);

        slashEffect.SetActive(true);
        Debug.Log("Slash effect activated!");

        Invoke(nameof(DeactivateSlashEffect), 0.2f); 
        readyToAttack = false;
        nextAttackTime = Time.time + attackCooldown;
        Invoke(nameof(ResetAttack), attackCooldown);
    }
    else
    {
        Debug.LogError("Slash effect, orientation transform, or slash position not assigned!");
    }
}


    private void DeactivateSlashEffect()
    {
        if (slashEffect != null)
        {
            slashEffect.SetActive(false);
            Debug.Log("Slash effect deactivated!");
        }
    }

    private void ResetAttack()
    {
        readyToAttack = true;
    }

    public IEnumerator Adrenaline(float time)
    {

        float defaultSpeed = moveSpeed;
        moveSpeed = moveSpeed + 20;

        yield return new WaitForSeconds(time);

        moveSpeed = defaultSpeed;
    } 


}
