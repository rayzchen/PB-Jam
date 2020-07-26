using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform cam;
    public float speed = 5f;
    public float camSpeed = 4f;
    public float stepSpeed = 0.5f;
    public Sprite[] textures;

    Vector2 movement;
    Rigidbody2D rb;
    Vector3 velocity = Vector3.zero;
    Camera camera;
    SpriteRenderer sr;
    float pose;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        camera = cam.gameObject.GetComponent<Camera>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (movement.magnitude > 1) movement = movement.normalized;
        cam.position = Vector3.SmoothDamp(cam.position, new Vector3(transform.position.x, transform.position.y, -10), ref velocity, 1 / camSpeed);
        camera.orthographicSize *= 1 + 0.01f * Input.GetAxis("Z axis");
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            sr.flipX = movement.x < 0;
        }

        pose += movement.magnitude * 5 * Time.deltaTime;
        pose %= 4;
        if (movement.magnitude < 0.01f) {
            pose = 4;
        }

        sr.sprite = textures[(int)pose];
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
