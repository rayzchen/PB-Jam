using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : MonoBehaviour {

    public Transform player;
    public PlayerController playerScript;
    public float minDistance = 3;
    public Chute chute;

    bool opening = false;
    Vector3 start;
    float vel;
    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        start = transform.position;
    }

    void Update() {
        if (Vector2.Distance(transform.position, player.position) < minDistance) {
            if (playerScript.items.Contains("Crowbar")) {
                if (Input.GetKey(KeyCode.O)) {
                    opening = true;
                }
            }
        }
        if (opening) {
            if (transform.localScale.x < 0.01f) {
                chute.enabled = true;
                Destroy(this);
            } else {
                transform.localScale = new Vector3(0, 1, 1) + Vector3.right * Mathf.SmoothDamp(transform.localScale.x, 0, ref vel, 1f);
                transform.position = start + Vector3.right * 2.75f * (1 - transform.localScale.x);
            }
        }
    }
}
