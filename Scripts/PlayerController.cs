using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform cam;
    public float speed = 5f;
    public float camSpeed = 4f;

    Vector2 movement;
    Rigidbody2D rb;
    Vector3 velocity = Vector3.zero;
    Camera camera;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        camera = cam.gameObject.GetComponent<Camera>();
    }

    void Update() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        cam.position = Vector3.SmoothDamp(cam.position, new Vector3(transform.position.x, transform.position.y, -10), ref velocity, 1 / camSpeed);
        camera.orthographicSize *= 1 + 0.01f * Input.GetAxis("Z axis");
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
