using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody))]
public class Player : Character
{
    Rigidbody playerBody;
    public int moveSpeed;
    float sqrMaxVelocity;
    bool noInput, up, down, left, right;
    float yaw = 0f, pitch = 0f;
    public float hSpeed = 2.0f, vSpeed = 2.0f;
    public Text hpText;
    Transform mainCamera;
    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        sqrMaxVelocity = (Vector3.forward * moveSpeed/2).sqrMagnitude;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main.transform;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ResetScene());
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKey(KeyCode.W) && !noInput)
        {
            up = true;
        }
        if (Input.GetKey(KeyCode.A) && !noInput)
        {
            left = true;
        }
        if (Input.GetKey(KeyCode.S) && !noInput)
        {
            down = true;
        }
        if (Input.GetKey(KeyCode.D) && !noInput)
        {
            right = true;
        }
    }
    private void FixedUpdate()
    {
        hpText.text = "HP: " + health + "/" + maxHealth;
        if (up && !noInput)
        {
            playerBody.AddRelativeForce(Vector3.forward * moveSpeed);
            up = false;
        }
        if (left && !noInput)
        {
            playerBody.AddRelativeForce(Vector3.left * moveSpeed * 0.8f);
            left = false;
        }
        if (down && !noInput)
        {
            playerBody.AddRelativeForce(Vector3.back * moveSpeed * 0.65f);
            down = false;
        }
        if (right && !noInput)
        {
            playerBody.AddRelativeForce(Vector3.right * moveSpeed * 0.8f);
            right = false;
        }
        noInput = playerBody.velocity.sqrMagnitude > sqrMaxVelocity ? true : false;
        //Debug.Log("Velocity: " + playerBody.velocity);
        yaw += hSpeed * Input.GetAxis("Mouse X");
        pitch -= vSpeed * Input.GetAxis("Mouse Y");
        if (pitch > 35)
            pitch = 35;
        else if (pitch < -85)
            pitch = -85;
        transform.eulerAngles = new Vector3(0, yaw, 0);
        mainCamera.localEulerAngles = new Vector3(pitch, 0, 0);
    }
    internal override void Die()
    {
        StartCoroutine(ResetScene());
    }
    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}