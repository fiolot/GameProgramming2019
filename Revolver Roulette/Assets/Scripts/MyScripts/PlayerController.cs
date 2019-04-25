using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RevolverRoulette
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
        internal Player player;
        private float movementSpeed, sideWalk, backWalk;
        private float pitch, yaw;
        [SerializeField]
        private Revolver revolver;
        [SerializeField]
        private Transform cameraMain;
        [SerializeField]
        float vSpeed, hSpeed;
        private void Start()
        {
            GameController.staticGameController.pController = this;
            movementSpeed = 0.1f;
            sideWalk = 0.8f;
            backWalk = 0.6f;
            player = GetComponent<Player>();
            StartCoroutine(revolver.VisualReload());
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        void Update()
        {
            //Mouse Input
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                revolver.TryFire();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                revolver.SkipChamber();
            }
            pitch -= Input.GetAxis("Mouse Y") * vSpeed;
            yaw += Input.GetAxis("Mouse X") * hSpeed;
            CheckRotation();
            SetRotation();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Time.timeScale = 20;
            }
            else
            {
                Time.timeScale = 1;
            }
            //Key Input
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
        }
        void CheckRotation()
        {
            pitch = pitch > 35 ? 35 : pitch;
            pitch = pitch < -85 ? -85 : pitch;
        }
        void SetRotation()
        {
            transform.eulerAngles = new Vector3(0, yaw, 0);
            cameraMain.localEulerAngles = new Vector3(pitch, 0, 0);
        }
    }
}