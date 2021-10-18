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
    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        distanceGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        realjumpValue = jumpValue/10;
        //A/D <-/-> Movement
        moveHorizontal = Input.GetAxis("Horizontal");
        //How fast you move on ground
        rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, 0);
        //Changes the physics material if the player is jumping
        if(jumpValue > 0)
        {
            coll.material.bounciness = 0.5f;
        }
        else
        {
            coll.material.bounciness = 0f;
        }

        //Increases jump height based on how long you hold space
        if(Input.GetButton("Jump") &&isGrounded&&canJump)
        {   
            isJump = 1;     
            rb.velocity = new Vector3(0f, 0f, 0f); 
            jumpValue += 0.1f;
        }
        if(Input.GetButtonDown("Jump")&& isGrounded && canJump)
        {
            isJump = 1;
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0f);
        }

        //Jump cap so you cant infinitely hold jump to get an insane jump height
        if(jumpValue >= 200f && isGrounded)
        {
            isJump = 0;
            float tempx = moveHorizontal * speed;
            float tempy = realjumpValue;
            rb.velocity = new Vector3(tempx, tempy, 0f);
            Invoke("ResetJump", 0.0f);
        }

        //When you release jump
        if(Input.GetButtonUp("Jump"))
        {    
            isJump = 0;
            //Lets you jump if you're grounded based on the jump value which was increased while holding space
            if (isGrounded)
            {
                rb.velocity = new Vector3(moveHorizontal * speed, realjumpValue, 0f);
                jumpValue = 0.0f;
            }
            canJump = true;
        }
    }
    //Fixed update to check if player is grounded and to affect player's gravity scale
    void FixedUpdate()
    {
        //Uses a raycast downward to check if the player is grounded or not
        if(!Physics.Raycast(transform.position, -Vector3.up, distanceGround + 0.1f))
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }

        //Modifies the players gravity
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    //Function to reset jump
    void ResetJump()
    {      
        canJump = false;
        jumpValue = 0;       
    }
}
