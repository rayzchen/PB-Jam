using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Controller : MonoBehaviour {
    public float speed = 5.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;
    bool cursorLocked;

    void Awake() {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 60;
    }

    void Start() {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    void Update() {
        float curSpeedX = speed * Input.GetAxis("Vertical");
        float curSpeedY = speed * Input.GetAxis("Horizontal");
        moveDirection = (transform.TransformDirection(Vector3.forward) * curSpeedX) + (transform.TransformDirection(Vector3.right) * curSpeedY) + transform.TransformDirection(Vector3.up) * moveDirection.y;

        if (characterController.isGrounded) {
            moveDirection.y = 0;
            if (Input.GetButton("Jump")) {
                moveDirection.y = jumpSpeed;
            } else {
                moveDirection.y = 0;
            }
        } else {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (cursorLocked) {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = Vector3.up * rotation.y;
        }
        
        characterController.Move(moveDirection * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) {
            cursorLocked = !cursorLocked;
            Cursor.lockState = (cursorLocked) ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !cursorLocked;
        }

        if (transform.position.y < -20) {
            transform.position = Vector3.up * 10;
            moveDirection = Vector3.zero;
            rotation = Vector2.zero;
        }
    }
}
