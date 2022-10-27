using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
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
    public bool readyToJump;
    [Header("Inputs")]
    float horizontalInput;
    float verticalInput;
    [Header("Assignable")]
    public Transform orientation;
    [HideInInspector] public Vector3 moveDirection;
    internal Rigidbody rb;
    [Header("Slope")] 
    public float maxSlopeAngle;
    RaycastHit slopeHit;
    public float slopeAngle;
    
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
    private void CheckIsGrounded() => grounded = Physics.CheckSphere(transform.position - new Vector3(0,1,0), 0.4f, groundLayer) && slopeAngle < maxSlopeAngle;
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
        //rb.useGravity = !onSlope();
        // rb.velocity when slope is bigger than maxSlope
        if (grounded && !onSlope())
        {
            rb.AddForce(moveDirection.normalized * (moveSpeed * 10));
        }
        else if (grounded && onSlope())
        {
            rb.AddForce(GetSlopeMoveDirection(moveDirection) * (moveSpeed * 10));
        }
        // skok
        else if (!grounded) rb.AddForce(moveDirection.normalized * (moveSpeed * airMultiplier * 1.5f));
        UpdateSlopeSliding();
    }

    private void UpdateSlopeSliding()
    {
        if (grounded)
        {
            if (slopeAngle > maxSlopeAngle)
            {
                onSlope();
                var normal = slopeHit.normal;
                var yInverse = 1 - normal.y;
                print(yInverse);
                rb.velocity = new Vector3(yInverse * normal.x * 2f, rb.velocity.y, yInverse * normal.z * 2f);
            }
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        // reset jump velocity 
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    internal IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(jumpCooldown);
        readyToJump = true;
    }

    public bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 100))
        {
            if (slopeHit.normal != Vector3.up)
            {
                slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return slopeAngle < maxSlopeAngle;
            }
            else
            {
                slopeAngle = 0f;
                return false;
            }
        }
        return false;
    }
    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}
