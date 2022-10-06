using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Vars")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    [Header("Keymap")]
    public KeyCode jumpKey = KeyCode.Space;
    [Header("Ground Check Related")]
    public float playerHeight;
    public LayerMask groundLayer;
    public bool grounded;
    bool readyToJump;
    [Header("Inputs")]
    float horizontalInput;
    float verticalInput;
    [Header("Assignable")]
    public Transform orientation;
    Vector3 moveDirection;
    internal Rigidbody rb;
    [Header("Slope")] 
    public float maxSlopeAngle;
    RaycastHit slopeHit;
    bool slopeExit;
    float slopeAngle;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }
    private void Update()
    {
        CheckIsGrounded();
        MyInput();
        SpeedControl();
        HandleDrag();
        onSlope();
    }
    // kinda helper functions
    private void CheckIsGrounded() => grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
    private void HandleDrag() => rb.drag = grounded ? groundDrag : 0;
    // ------------------------
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        HandleJump();
    }
    private void HandleJump()
    {
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            // here should be audio effect
            Jump();
            StartCoroutine(ResetJump());
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        // somewhere here should be sound effect
        // slope
        rb.useGravity = !onSlope();
        if (onSlope() && !slopeExit && grounded)
        {
            rb.AddForce(GetSlopeMoveDirection() * (moveSpeed * 10 * 1.5f));
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 100f);
            }
        }

        if (!onSlope() && !grounded && slopeAngle > maxSlopeAngle)
        {
            rb.velocity = new Vector3(0, -4f,0);
        }
        if (grounded && !onSlope())
        {
            rb.AddForce(moveDirection.normalized * (moveSpeed * 10));
        }
        // skok
        else if (!grounded) rb.AddForce(moveDirection.normalized * (moveSpeed * airMultiplier * 1.5f));
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // slope handling
        if(onSlope() && !slopeExit)
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        slopeExit = true;
        // reset jump velocity 
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    internal IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(jumpCooldown);
        readyToJump = true;
        slopeExit = false;
    }

    private bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return slopeAngle < maxSlopeAngle && slopeAngle != 0;
        }
        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}