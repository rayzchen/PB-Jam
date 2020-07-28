using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Transform cam;
    public float speed = 5f;
    public float camSpeed = 4f;
    public float stepSpeed = 0.5f;
    public float sprintSpeed = 1.5f;
    public Sprite[] textures;
    public Sprite[] textures_walk;
    public Sprite[] textures_search;
    public float timeBetweenSearchFrames = 1;

    Vector2 movement;
    Rigidbody2D rb;
    Vector3 velocity = Vector3.zero;
    Camera camera;
    SpriteRenderer sr;
    float pose;
    Vector4 camBounds;
    float frame = 0;

    [HideInInspector] public List<string> pickedUp;
    [HideInInspector] public bool searching = false;
    [HideInInspector] public List<string> items;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        camera = cam.gameObject.GetComponent<Camera>();
        sr = GetComponent<SpriteRenderer>();
        pickedUp = new List<string>();
        items = new List<string>();
    }

    void Update() {
        if (!searching) {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            if (movement.magnitude > 1) movement = movement.normalized;
            if (Input.GetKey(KeyCode.LeftShift)) movement *= sprintSpeed;
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                sr.flipX = movement.x < 0;
            }

            pose += movement.magnitude * 5 * Time.deltaTime;
            pose %= 4;
            if (movement.magnitude < 0.01f) {
                pose = 4;
            }

            if (Input.GetKey(KeyCode.LeftShift)) {
                sr.sprite = textures_walk[(int)pose];
            }  else {
                sr.sprite = textures[(int)pose];
            }

            float screenAspect = (float) Screen.width / (float) Screen.height;
            float camHalfWidth = camera.orthographicSize * screenAspect;

            camBounds = new Vector4(cam.position.x - camHalfWidth + 4, cam.position.x + camHalfWidth - 4, cam.position.y + camera.orthographicSize - 6, cam.position.y - camera.orthographicSize + 2);
            if (transform.position.x < camBounds.x) {
                cam.position -= Vector3.right * (camBounds.x - transform.position.x);
            } else if (transform.position.x > camBounds.y) {
                cam.position += Vector3.right * (transform.position.x - camBounds.y);
            }
            if (transform.position.y > camBounds.z) {
                cam.position += Vector3.up * (transform.position.y - camBounds.z);
            } else if (transform.position.y < camBounds.w) {
                cam.position -= Vector3.up * (camBounds.w - transform.position.y);
            }

            if (Input.GetKey(KeyCode.Q)) {
                searching = true;
                movement = Vector2.zero;
            }
        } else {
            frame += 1 / timeBetweenSearchFrames * Time.deltaTime;
            if (Input.GetKey(KeyCode.C) || frame > textures_search.Length * 2) {
                searching = false;
                if (!Input.GetKey(KeyCode.C)) {
                    foreach (string item in pickedUp) {
                        print("Picked up " + item);
                        items.Add(item);
                    }
                }
                frame = 0;
            }
            sr.sprite = textures_search[(int)(frame % textures_search.Length)];
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
