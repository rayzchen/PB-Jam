using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    public Transform player;
    public float minDistance = 3;
    public float maxDistance = 10;

    float time = 0;
    AudioSource audio;

    void Start() {
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.E) && Vector2.Distance(player.position, transform.position) < minDistance) {
            time = 0;
            if (!audio.isPlaying) {
                audio.Play();
            }
        }
        time += Time.deltaTime;
        if (time > 20f || (time > 0f && Vector2.Distance(player.position, transform.position) > maxDistance)) {
            audio.Stop();
        }
    }
}
