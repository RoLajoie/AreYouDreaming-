using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    Vector3 playerVelocity;
    Vector3 move;

    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpHeight = 2;
    public int maxJumpCount = 1;
    public float gravity = -9.18f;
    public bool isGrounded;
    public bool isRunning;
    
    public AudioSource jump; 

    public float doubleJumpTimer = 0; 
    public bool doubleJump;
    public bool hasDoubleJumped; 
    //Blue Thing gameobject to touch 
    private GameObject blueThing;

    private CharacterController controller;
    private Animator animator;
    public Transform cameraTransform;

    private float groundCheckDistance = 0.1f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void ProcessMovement()
    {
        //changes isGrounded to a raycast that points downwards to check distance between player and ground 
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        
        if (isGrounded && playerVelocity.y < 0)
        {   
            //this allows the player velocity to appear slightly in the ground so it's always able to jump even on a crooked surface
            playerVelocity.y = -1f;
            //Debug.Log("Player Velocty Subtracted");
        }

        //get raw input for movement
        float moveX = Input.GetAxis("Horizontal");  
        float moveZ = Input.GetAxis("Vertical");    

        
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

       
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        // movement direction based on camera forward and right
        move = (cameraForward * moveZ) + (cameraRight * moveX);

        //when moving, rotate the player towards the movement direction
        if (move != Vector3.zero)
        {
            //rotate player to face movement direction
            transform.forward = move;
        }
       
        isRunning = Input.GetButton("Run");
        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        float speed = Mathf.Lerp(controller.velocity.magnitude, targetSpeed, 1f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            jump.Play(); 
            doubleJump = doubleJumpTimer > 0;

        } else if(Input.GetButtonDown("Jump") && doubleJump && doubleJumpTimer > 0)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            jump.Play(); 
            doubleJump = false;
            animator.SetTrigger("doubleJump"); 
        }

        controller.Move(move * Time.deltaTime * speed);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("BlueThing"))
        {

            BluePickup bluePickup = other.gameObject.GetComponent<BluePickup>();

            if (bluePickup != null)
            {
                bluePickup.playSound(transform.position);
                blueThing = other.gameObject;
                doubleJumpTimer = 30f; 
                blueThing.SetActive(false);  
            }
        }

        
        if (other.gameObject.CompareTag("YellowThing"))
        {
            YellowPickup yellowPickup = other.gameObject.GetComponent<YellowPickup>();

            if (yellowPickup != null)
            {
                yellowPickup.yellowInteraction(transform.position); 

                other.gameObject.SetActive(false);  
            }
        }
    }

    

    // DONT MODIFY -------------------------------------------------------
    public void Update()
    {
        isGrounded = controller.isGrounded;

        if (doubleJumpTimer > 0)
        {
            doubleJumpTimer -= Time.deltaTime;
            
            if (doubleJumpTimer <= 0 && blueThing != null)
            {
                blueThing.SetActive(true); 
                blueThing = null; 
            }
        }

        if (animator.applyRootMotion == false)
        {
            ProcessMovement();
        }
        ProcessGravity();

    }

    public void ProcessGravity()
    {

        // Since there is no physics applied on character controller we have this applies to reapply gravity
        if (isGrounded)
        {
            if (playerVelocity.y < 0.0f) // we want to make sure the players stays grounded when on the ground
            {
                playerVelocity.y = -1.0f;
            }
        }
        else // if not grounded
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
        isGrounded = controller.isGrounded;

    }

    private void OnAnimatorMove()
    {
        Vector3 velocity = animator.deltaPosition;
        velocity.y = playerVelocity.y * Time.deltaTime;

        controller.Move(velocity);
    }

    public float GetAnimationSpeed()
    {
        if (isRunning && (move != Vector3.zero))// Left shift
        {
            return 1.0f;
        }
        else if (move != Vector3.zero)
        {
            return 0.5f;
        }
        else 
        {
            return 0f;
        }
    }
    // ---------------------------------------------------------------------------

}
