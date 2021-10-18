using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public player movement variables
    public int speed;
    public int baseSpeed = 5;
    public bool isGrounded;
    public float jumpHeight;
    public bool canJump = true;
    public float jumpValue = 0.0f;

    //Ground mask to determine if player is grounded
    public LayerMask groundMask;

    //Private player variables
    private Rigidbody rb;
    private float moveHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //A/D <-/-> Movement
        moveHorizontal = Input.GetAxis("Horizontal");
        //How fast you move on ground
        rb.velocity = new Vector3(moveHorizontal * speed, rb.velocity.y, 0);
        /*Determines if the player is grounded, need to maybe use raycasts because this is similar to how I did it in my 2d game but I don't think it'll work in 3d
        isGrounded = Physics.OverlapBox(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z),
        new Vector3(0.9f, 0.4f, 0f), Quaternion.identity, groundMask);
        */
    }
}
