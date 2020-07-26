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
    int direction;
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
            if (movement.x < movement.y) { if (-movement.x < movement.y) { direction = 2; } else { direction = 1; }
            } else { if (-movement.x > movement.y) { direction = 0; } else { direction = 3; }}
        }

        pose += movement.magnitude / 0.25f * Time.deltaTime;
        pose %= 4;
        if (movement.magnitude < 0.01f) {
            pose = 0;
        }

        sr.sprite = textures[(int)pose * 4 + direction];
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
