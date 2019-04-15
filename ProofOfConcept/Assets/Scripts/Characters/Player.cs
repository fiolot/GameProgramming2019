using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Player : Character
{
    Rigidbody playerBody;
    public int moveSpeed;
    float sqrMaxVelocity;
    bool noInput;
    float yaw = 0f;
    public float hSpeed = 2.0f;
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        sqrMaxVelocity = (Vector3.forward * moveSpeed).sqrMagnitude;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKey(KeyCode.W) && !noInput)
        {
            playerBody.AddRelativeForce(Vector3.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) && !noInput)
        {
            playerBody.AddRelativeForce(Vector3.left * moveSpeed * 0.8f);
        }
        //if (Input.GetKey(KeyCode.S) && !noInput)
        //{
        //    playerBody.AddForce(Vector3.back * moveSpeed);
        //}
        if (Input.GetKey(KeyCode.D) && !noInput)
        {
            playerBody.AddRelativeForce(Vector3.right * moveSpeed * 0.8f);
        }
        noInput = playerBody.velocity.sqrMagnitude > sqrMaxVelocity ? true : false;
        //Debug.Log("Velocity: " + playerBody.velocity);
        yaw += hSpeed * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0, yaw, 0);
    }
}