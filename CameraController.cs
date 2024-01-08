/*
Ortographic Camera Controller for Unity Game Engine
===================================================
*Created on 08.01.2024 by Tobias Witte*
*Copyright (C) 2024*
*For COPYING and LICENSE details, please refer to the LICENSE file*
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camTransform;
    public GameObject camRig;

    public float movementSpeed;
    public float movementTime;
    public float rotAmount;
    public Vector3 zoomAmt;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public bool isMovementLocked;

    public Camera cam;

    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 1f;
    private float maxZoom = 10f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;


    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = camTransform.localPosition;
        zoom = cam.orthographicSize;
    }

    void LateUpdate()
    {
        HandleMovementInput();
        HandleMouseInput();
    }

    public void LockMovement()
    {
        isMovementLocked = true;
    }

    public void UnlockMovement()
    {
        isMovementLocked = false;
    }

    public void HandleMouseInput()
    {

    }

    public void HandleMovementInput()
    {
        if (isMovementLocked == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += (transform.forward * movementSpeed);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition += (transform.forward * -movementSpeed);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += (transform.right * movementSpeed);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition += (transform.right * -movementSpeed);
            }
            
            if(Input.GetKey(KeyCode.LeftShift))
            {
                movementSpeed = 0.3f;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                movementSpeed = 0.05f;
            }
            else 
            {
                movementSpeed = 0.2f;
            }
            

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            zoom -= scroll * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                newRotation *= Quaternion.Euler(Vector3.up * 90);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                newRotation *= Quaternion.Euler(Vector3.up * -90);
            }

            //if (Input.GetKey(KeyCode.R) || Input.GetAxis("Mouse ScrollWheel") > 0f) // && newZoom.y > 10)
            //{
                //newZoom += zoomAmt;
            //    mainCam.orthographicSize = Mathf.SmoothStep(mainCam.orthographicSize, 5.0f, Time.deltaTime);
            //}
            //if (Input.GetKey(KeyCode.F) || Input.GetAxis("Mouse ScrollWheel") < 0f) // && newZoom.y < 70)
            //{
                //newZoom -= zoomAmt;
            //    mainCam.orthographicSize = -Mathf.SmoothStep(mainCam.orthographicSize, 5.0f, Time.deltaTime);
            //}


            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
            //camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        }
    }
}
