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
    private bool grounded;
    bool readyToJump;
    [Header("Inputs")]
    float horizontalInput;
    float verticalInput;
    [Header("Assignable")]
    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;
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
        if (grounded) rb.AddForce(moveDirection.normalized * (moveSpeed * 10));
        // skok
        else if (!grounded) rb.AddForce(moveDirection.normalized * (moveSpeed * airMultiplier));
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
}
