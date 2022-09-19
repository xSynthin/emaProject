using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Sensitivity")]
    public float sensX;
    public float sensY;
    [Header("Assignable")]
    public Transform orientation;
    public Transform camHolder;
    public PlayerController pController;
    float xRotation;
    float yRotation;
    public Transform camT;
    private Vector3 bobVector;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private float minBobVal = 10f;
    private float counter;
    private float idleCounter;
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        CameraSway();
    }

    private void CameraSway()
    {
        //float vertical = Input.GetAxisRaw("Vertical");
        // TODO this needs to be correlated with move speed entirely
        // probably with use of animation curves
        if (pController.rb.velocity.magnitude > minBobVal && pController.grounded)
        {
            HeadBob(counter, 0.05f, 0.05f);
            counter += Time.deltaTime * pController.moveSpeed / 2f;
            camT.localPosition = Vector3.Lerp(camT.localPosition, bobVector, Time.deltaTime * 6f);
        }
        else
        {
            HeadBob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            camT.localPosition = Vector3.Lerp(camT.localPosition, bobVector, Time.deltaTime * 2f);
        }
        // TODO change camera rotation while strafing/running in other dirs that forward/backward
    }
    private void HeadBob(float period, float vAmplitude, float hAmplitude)
    {
        bobVector = new Vector3(Mathf.Cos(period) * vAmplitude, Mathf.Sin(period*2) * hAmplitude, camT.localPosition.z);
    }
}