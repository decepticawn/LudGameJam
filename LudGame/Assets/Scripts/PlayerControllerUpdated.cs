using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUpdated : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private bool rawInput;
    [SerializeField] private bool jump;
    [SerializeField] private bool airControls;
    
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float playerGravity;

    [SerializeField] private Transform[] groundRaycastPoints;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private bool isGrounded;

    [SerializeField] private Rigidbody playerRb;

    private void Start()
    {
        Debug.Log("Current Gravity : " + Physics.gravity);
    }

    private void Update()
    {
        HandlePlayerInput();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Physics.gravity = new Vector3(Physics.gravity.x, playerGravity, Physics.gravity.z);
        MovePlayer();
    }

    private void HandlePlayerInput()
    {
        if (airControls)
        {
            horizontalInput = rawInput ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        }
        else
        {
            if (isGrounded)
            {
                horizontalInput = rawInput ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
    }

    private void GroundCheck()
    {
        for (int i = 0; i < groundRaycastPoints.Length; i++)
        {
            if (Physics.Raycast(groundRaycastPoints[i].position, -groundRaycastPoints[i].up, .3f, groundLayerMask))
            {
                isGrounded = true;
                return;
            }
        }
        
        isGrounded = false;
    }
    
    private void MovePlayer()
    {
        float verticalVelocity = CalculateVerticalVelocity();
        playerRb.velocity = new Vector3(horizontalInput * playerMoveSpeed, verticalVelocity, 0);
    }

    private float CalculateVerticalVelocity()
    {
        if (jump)
        {
            jump = false;
            float jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            return jumpForce;
        }

        return playerRb.velocity.y;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < groundRaycastPoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(groundRaycastPoints[i].position, -groundRaycastPoints[i].up * .3f);
        }
    }
}
