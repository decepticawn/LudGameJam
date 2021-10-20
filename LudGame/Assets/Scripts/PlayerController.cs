using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public player movement variables
    public int speed;
    public int baseSpeed = 5;
    public float jumpHeight;
    public bool canJump = true;
    public float jumpValue = 0.0f;
    public float realjumpValue = 0.0f;
    public float isJump = 0;

    //Variables used to check if the player is grounded
    public float distanceGround;
    public bool isGrounded  = false;

    //Physics material
    public PhysicMaterial playerMat;

    //Gravity scale since unity 3d doesnt have one on rigidbodies
    public float gravityScale = 4.0f;
    public static float globalGravity = -9.81f;

    //Private player variables
    private Rigidbody rb;
    private Collider coll;
    private float moveHorizontal;
    private bool rawInput;
    [SerializeField] private float groundedOffset;
    [SerializeField] private float groundedRadius;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private bool jumpHeldDown = false;
    [SerializeField] private bool jump = false;
    [SerializeField] private float MIN_JUMP_FORCE = 10;
    [SerializeField] private float MAX_JUMP_FORCE = 100;
    [SerializeField] private float jumpForceIncrementFactor = 10;
    [SerializeField] private float jumpForce;
    

    void Start()
    {
        SetupPlayerComponents();
        InitializePlayerDefaults();
    }

    private void Update()
    {
        HandleInput();
        GroundCheck();
        HandleJumpForce();
        invisWall();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void SetupPlayerComponents()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void InitializePlayerDefaults()
    {
        speed = baseSpeed;
        distanceGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void HandleInput()
    {
        moveHorizontal = rawInput ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpHeldDown = true;
        }

        else if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            MakeJump();
        }
    }

    private void MakeJump()
    {
        jumpHeldDown = false;
        jump = true;
    }

    private void MovePlayer()
    {
        float yVelocity = rb.velocity.y;
        if (jump && isGrounded)
        {
            jump = false;
            yVelocity = jumpForce;
            jumpForce = 0;
        }
        rb.velocity = new Vector3(moveHorizontal * speed, yVelocity, 0);
    }
    
    private void GroundCheck()
    {
        Vector3 boxPosition = new Vector3(transform.position.x, 
            transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckBox(boxPosition, new Vector3(.5f, .5f, .5f), 
            Quaternion.identity, groundLayer);
    }

    private void HandleJumpForce()
    {
        if (jumpHeldDown)
        {
            jumpForce += Time.deltaTime * jumpForceIncrementFactor;
            // the square root of H * -2 * G = how much velocity needed to reach desired height
            //float verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpForce = Mathf.Clamp(jumpForce, MIN_JUMP_FORCE, MAX_JUMP_FORCE);
            if (jumpForce >= MAX_JUMP_FORCE)
            {
                MakeJump();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;
			
        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawCube(new Vector3(transform.position.x, 
            transform.position.y - groundedOffset, transform.position.z), new Vector3(1, 1, 1));
    }

    private void invisWall()
    {
        //Left side
        if(transform.position.x < -9.572674f)
        {
            transform.position = new Vector3(-9.572674f, transform.position.y, transform.position.z);
        }
        //Right side
        if (transform.position.x > 9.539885f)
        {
            transform.position = new Vector3(9.539885f, transform.position.y, transform.position.z);
        }
    }
    
    // void Update()
    // {
    //     realjumpValue = jumpValue/10;
    //     //A/D <-/-> Movement
    //     moveHorizontal = Input.GetAxis("Horizontal");
    //     //How fast you move on ground
    //     rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, 0);
    //     //Changes the physics material if the player is jumping
    //     if(jumpValue > 0)
    //     {
    //         coll.material.bounciness = 0.5f;
    //     }
    //     else
    //     {
    //         coll.material.bounciness = 0f;
    //     }
    //
    //     //Increases jump height based on how long you hold space
    //     if(Input.GetButton("Jump") &&isGrounded&&canJump)
    //     {   
    //         isJump = 1;     
    //         rb.velocity = new Vector3(0f, 0f, 0f); 
    //         jumpValue += 0.1f;
    //     }
    //     if(Input.GetButtonDown("Jump")&& isGrounded && canJump)
    //     {
    //         isJump = 1;
    //         rb.velocity = new Vector3(0.0f, rb.velocity.y, 0f);
    //     }
    //
    //     //Jump cap so you cant infinitely hold jump to get an insane jump height
    //     if(jumpValue >= 200f && isGrounded)
    //     {
    //         isJump = 0;
    //         float tempx = moveHorizontal * speed;
    //         float tempy = realjumpValue;
    //         rb.velocity = new Vector3(tempx, tempy, 0f);
    //         Invoke("ResetJump", 0.0f);
    //     }
    //
    //     //When you release jump
    //     if(Input.GetButtonUp("Jump"))
    //     {    
    //         isJump = 0;
    //         //Lets you jump if you're grounded based on the jump value which was increased while holding space
    //         if (isGrounded)
    //         {
    //             rb.velocity = new Vector3(moveHorizontal * speed, realjumpValue, 0f);
    //             jumpValue = 0.0f;
    //         }
    //         canJump = true;
    //     }
    // }
    //
    // void FixedUpdate()
    // {
    //     //Uses a raycast downward to check if the player is grounded or not
    //     if(!Physics.Raycast(transform.position, -Vector3.up, distanceGround + 0.1f))
    //     {
    //         isGrounded = false;
    //     }
    //     else
    //     {
    //         isGrounded = true;
    //     }
    //
    //     //Modifies the players gravity
    //     Vector3 gravity = globalGravity * gravityScale * Vector3.up;
    //     rb.AddForce(gravity, ForceMode.Acceleration);
    // }
    
    // void ResetJump()
    // {      
    //     canJump = false;
    //     jumpValue = 0;       
    // }
}
