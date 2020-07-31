using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public PlayerController player;
    public Sprite[] textures;
    public float minDistance = 3;

    bool open = false;
    SpriteRenderer sr;
    BoxCollider2D coll;
    int delay = 0;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update() {
        if (delay == 0) {
            if (Input.GetKey(KeyCode.O) && Vector2.Distance(player.transform.position, transform.position) < minDistance && player.items.Contains("Key")) {
                open = !open;
                coll.enabled = open;
                switch (open) {
                    case true: sr.sprite = textures[0]; break;
                    case false: sr.sprite = textures[1]; break;
                }
                delay = 20;
            }
        } else {
            delay -= 1;
        }
    }
}
