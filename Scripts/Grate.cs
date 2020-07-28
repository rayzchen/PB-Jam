using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grate : MonoBehaviour {

    public Transform player;
    public float minDistance = 3;

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
            if (Input.GetKey(KeyCode.O)) {
                opening = true;
            }
        }
        if (opening) {
            if (transform.localScale.x < 0.01f) {
                Chute chute = gameObject.AddComponent<Chute>();
                chute.player = player;
                chute.minDistance = minDistance;
                Destroy(this);
            } else {
                transform.localScale = new Vector3(0, 1, 1) + Vector3.right * Mathf.SmoothDamp(transform.localScale.x, 0, ref vel, 1f);
                transform.position = start + Vector3.right * (1 - transform.localScale.x);
            }
        }
    }
}
