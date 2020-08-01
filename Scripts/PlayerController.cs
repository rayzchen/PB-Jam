using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioClip[] sounds;
    public float dampening = 2;
    public Color invisibleColor;
    public Color normalColor;
    public Text itemText;

    Vector2 movement;
    Rigidbody2D rb;
    Vector2 vel = Vector2.zero;
    Camera camera;
    SpriteRenderer sr;
    float pose;
    float frame = 0;
    AudioSource audio;
    bool started = false;

    [HideInInspector] public List<string> pickedUp;
    [HideInInspector] public bool searching = false;
    [HideInInspector] public List<string> items;
    [HideInInspector] public bool sprinting = false;

    IEnumerator Start() {
        rb = GetComponent<Rigidbody2D>();
        camera = cam.gameObject.GetComponent<Camera>();
        sr = GetComponent<SpriteRenderer>();
        pickedUp = new List<string>();
        items = new List<string>();
        audio = GetComponent<AudioSource>();
        audio.volume = 0.6f;
        yield return new WaitForSeconds(2f);
        started = true;
    }

    void Update() {
        if (started) {
            cam.position = (Vector3)Vector2.SmoothDamp(cam.position, transform.position + Vector3.up * 2, ref vel, dampening) + Vector3.forward * -10;
            if (!searching) {
                movement.x = Input.GetAxis("Horizontal");
                movement.y = Input.GetAxis("Vertical");
                if (movement.magnitude > 1) movement = movement.normalized;
                if (Input.GetKey(KeyCode.LeftShift)) movement *= sprintSpeed;
                
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                    sr.flipX = movement.x < 0;
                    sprinting = Input.GetKey(KeyCode.LeftShift);
                    if (sprinting) audio.volume = 1f;
                    else audio.volume = 0.6f;
                } else {
                    sprinting = false;
                }

                int lastStep = (int)(pose / 2);
                pose += movement.magnitude * 5 * Time.deltaTime;
                pose %= 4;
                if (movement.magnitude < 0.01f) {
                    pose = 4;
                }

                if ((int)(pose / 2) != lastStep) {
                    audio.Stop();
                    if (audio.clip == sounds[0]) audio.clip = sounds[1];
                    else if (audio.clip == sounds[1]) audio.clip = sounds[0];
                    audio.Play();
                }

                if (Input.GetKey(KeyCode.LeftShift)) {
                    sr.sprite = textures_walk[(int)pose];
                }  else {
                    sr.sprite = textures[(int)pose];
                }

                if (items.Contains("Potion") && Input.GetKey(KeyCode.U)) {
                    items.Remove("Potion");
                    StartCoroutine(InvisibilityPotion());
                }

                string temp = "Items: ";
                if (items.Count == 0) {
                    temp += "None";
                } else {
                    foreach (string item in items) {
                        temp += item + " ";
                    }
                }
                itemText.text = temp;

                if (Input.GetKey(KeyCode.E)) {
                    searching = true;
                    movement = Vector2.zero;
                }
            } else {
                frame += 1 / timeBetweenSearchFrames * Time.deltaTime;
                if (Input.GetKey(KeyCode.C) || frame > textures_search.Length * 2) {
                    searching = false;
                    foreach (string item in pickedUp) {
                        print("Picked up " + item);
                        items.Add(item);
                    }
                    frame = 0;
                    pickedUp.Clear();
                }
                sr.sprite = textures_search[(int)(frame % textures_search.Length)];
            }
        }
    }

    IEnumerator InvisibilityPotion() {
        gameObject.layer = 2;
        sr.color = invisibleColor;
        yield return new WaitForSeconds(10f);
        gameObject.layer = 8;
        sr.color = normalColor;
    }

    public void CancelInvisibility() {
        StopAllCoroutines();
        gameObject.layer = 8;
        sr.color = normalColor;
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
