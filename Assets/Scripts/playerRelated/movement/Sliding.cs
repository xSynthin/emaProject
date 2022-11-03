using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("REFS")] 
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerController pc;
    [Header("Sliding Parameters")] 
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;
    public float slideYScale;
    private float startYScale;
    [Header("Input")] 
    public KeyCode slideKey = KeyCode.LeftShift;
    private float horizontalInput;
    private float verticalInput;
    private bool sliding;
    private Vector3 inputDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        SlideInputs();
    }

    private void SlideInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && pc.readyToJump)
            StartSlide();
        if(Input.GetKeyUp(slideKey) && sliding)
            StopSlide();
    }

    private void FixedUpdate()
    {
        if(sliding)
            SlidingMov();
    }

    private void StartSlide()
    {
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        sliding = true;
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        slideTimer = maxSlideTime;
    }

    private void SlidingMov()
    {
            if (!pc.onSlope() || pc.slopeAngle > pc.maxSlopeAngle)
            {
                rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
                slideTimer -= Time.deltaTime;
            }
            else
            {
                if(rb.velocity.y < -0.5f){
                    rb.AddForce(pc.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
                }
                else
                {
                    slideTimer -= Time.deltaTime;
                }
            }
            if (!(slideTimer <= 0)) return;
            StopSlide();
    }

    private void StopSlide()
    {
        sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
