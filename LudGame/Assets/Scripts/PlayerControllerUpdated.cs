using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerUpdated : MonoBehaviour
{
    [SerializeField] private float horizontalInput;
    [SerializeField] private bool rawInput;
    [SerializeField] private bool jump;
    [SerializeField] private bool airControls;
    
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float MIN_JUMP_HEIGHT = 2;
    [SerializeField] private float MAX_JUMP_HEIGHT = 8;
    [SerializeField] private float jumpHeightIncreaseFactor = 5;
    [SerializeField] private float playerGravity;

    [SerializeField] private Transform[] groundRaycastPoints;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private bool isGrounded;

    [SerializeField] private Rigidbody playerRb;

    [SerializeField] private Image chargedJumpFillImage;

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

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jumpHeight += jumpHeightIncreaseFactor * Time.deltaTime;
            jumpHeight = Mathf.Clamp(jumpHeight, MIN_JUMP_HEIGHT, MAX_JUMP_HEIGHT);
            chargedJumpFillImage.fillAmount = Remap(jumpHeight, MIN_JUMP_HEIGHT, MAX_JUMP_HEIGHT, 0, 1);
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
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
            jumpHeight = MIN_JUMP_HEIGHT;
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
    
    public static float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
