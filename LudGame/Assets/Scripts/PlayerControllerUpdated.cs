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
    [SerializeField] private float MIN_GRAVITY = -50;
    [SerializeField] private float MAX_GRAVITY = -100;

    [SerializeField] private Transform[] groundRaycastPoints;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private bool isGrounded;

    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Transform playerGFXHolderTransform;
    [SerializeField] private TrailRenderer trail;
    
    [SerializeField] private Transform[] leftRaycastPoints;
    [SerializeField] private Transform[] rightRaycastPoints;
    [SerializeField] private bool blockedLeft, blockedRight;

    [SerializeField] private Image chargedJumpFillImage;
    [SerializeField] private AudioSource jumpNoise;
    [SerializeField] private GameObject playerTransform;
    SavePos playerPosData;
    public Heavy heavy;

    private void Awake()
    {
        loadSave();
    }
    private void Start()
    {     
        Debug.Log("Current Gravity : " + Physics.gravity);
    }

    private void Update()
    {
        
        PlayerPosSave(playerTransform.transform.position.x, playerTransform.transform.position.y, playerTransform.transform.position.z);
        if(heavy.isHeavy)
        {
            horizontalInput = 0;
            playerRb.velocity = new Vector3(0f, -50f, 0f);
        }
        if(heavy.isHeavy == false)
        {
            HandlePlayerInput();
        }
        GroundCheck();
        SideWaysRaycasts();
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

        if (blockedLeft && horizontalInput < 0)
        {
            horizontalInput = 0;
        }

        if (blockedRight && horizontalInput > 0)
        {
            horizontalInput = 0;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            jumpHeight += jumpHeightIncreaseFactor * Time.deltaTime;
            jumpHeight = Mathf.Clamp(jumpHeight, MIN_JUMP_HEIGHT, MAX_JUMP_HEIGHT);
            chargedJumpFillImage.fillAmount = Remap(jumpHeight, MIN_JUMP_HEIGHT, MAX_JUMP_HEIGHT, 0, 1);
            HandlePlayerScale();
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            jumpNoise.Play();
            jump = true;
        }
    }

    private void GroundCheck()
    {
        for (int i = 0; i < groundRaycastPoints.Length; i++)
        {
            if (Physics.Raycast(groundRaycastPoints[i].position, -groundRaycastPoints[i].up, out RaycastHit hitInfo, .3f, groundLayerMask))
            {
                if (hitInfo.collider.GetComponentInChildren<MovingPlatform>())
                {
                    transform.SetParent(hitInfo.collider.transform);
                }
                else
                {
                    if (transform.parent != null)
                    {
                        transform.parent = null;
                    }
                }
                if (!isGrounded)
                {
                    GroundSquash();
                }
                isGrounded = true;
                return;
            }
        }
        
        if (transform.parent != null)
        {
            transform.parent = null;
        }
        isGrounded = false;
    }

    private void SideWaysRaycasts()
    {
        LeftSideRaycasts();
        RightSideRaycasts();
    }

    private void LeftSideRaycasts()
    {
        for (int i = 0; i < leftRaycastPoints.Length; i++)
        {
            if (Physics.Raycast(leftRaycastPoints[i].position, -leftRaycastPoints[i].right, out RaycastHit hitInfo, .2f, groundLayerMask))
            {
                blockedLeft = true;
                return;
            }
        }

        blockedLeft = false;
    }

    private void RightSideRaycasts()
    {
        for (int i = 0; i < rightRaycastPoints.Length; i++)
        {
            if (Physics.Raycast(rightRaycastPoints[i].position, rightRaycastPoints[i].right, out RaycastHit hitInfo, .2f, groundLayerMask))
            {
                blockedRight = true;
                return;
            }
        }

        blockedRight = false;
    }
    
    private void MovePlayer()
    {
        float verticalVelocity = CalculateVerticalVelocity();
        verticalVelocity = Mathf.Clamp(verticalVelocity, -100, 100);
        playerRb.velocity = new Vector3(horizontalInput * playerMoveSpeed, verticalVelocity, 0);
    }

    private float CalculateVerticalVelocity()
    {
        if (jump)
        {
            jump = false;
            float jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
            jumpHeight = MIN_JUMP_HEIGHT;
            HandlePlayerScale();
            return jumpForce;
        }

        return playerRb.velocity.y;
    }

    private void HandlePlayerScale()
    {
        float yScale = Remap(jumpHeight, MIN_JUMP_HEIGHT, MAX_JUMP_HEIGHT, 1f, .5f);
        playerGFXHolderTransform.localScale = new Vector3(playerGFXHolderTransform.localScale.x, yScale,
            playerGFXHolderTransform.localScale.z);
        trail.startWidth = yScale;
        trail.endWidth = yScale;
    }

    private void GroundSquash()
    {
        if (playerGFXHolderTransform.localScale.y < 1f)
        {
            return;
        }
        playerGFXHolderTransform.LeanScaleY(.8f, .1f).setOnComplete(() =>
        {
            playerGFXHolderTransform.LeanScaleY(1f, .1f);
        });
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < groundRaycastPoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(groundRaycastPoints[i].position, -groundRaycastPoints[i].up * .3f);
        }
        
        for (int i = 0; i < leftRaycastPoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(leftRaycastPoints[i].position, -leftRaycastPoints[i].right * .2f);
        }
        
        for (int i = 0; i < rightRaycastPoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(rightRaycastPoints[i].position, rightRaycastPoints[i].right * .2f);
        }
    }

    private void loadSave()
    {
        playerPosData = FindObjectOfType<SavePos>();
        playerPosData.PlayerPosLoad();
    }
    
    public static float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Wind")
        {
            heavy.canHeavy = true;
        }
        else if(col.tag == "BeforeWind")
        {
            heavy.canHeavy = false;
        }
    }

    public void PlayerPosSave(float x, float y, float z)
    {
        PlayerPrefs.SetFloat("p_x", x);
        PlayerPrefs.SetFloat("p_y", y);
        PlayerPrefs.SetFloat("p_z", z);
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
    }
}