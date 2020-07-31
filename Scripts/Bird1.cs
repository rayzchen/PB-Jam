using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird1 : MonoBehaviour {

    public Transform player;
    public Sprite[] textures;
    public Sprite[] fly_textures;
    public Sprite[] land_textures;
    public float timeBetweenFrames = 0.25f;
    public float frame = 0;

    SpriteRenderer sr;
    bool flyingaway = false;
    bool flyingback = false;
    AudioSource audio;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        if (Vector2.Distance(transform.position, player.position) > 3) {
            if (sr.enabled && !flyingback) {
                frame += 1 / timeBetweenFrames * Time.deltaTime;
                frame %= textures.Length;
                sr.sprite = textures[(int)frame];
            } else if (Vector2.Distance(transform.position, player.position) > 7) {
                if (!sr.enabled) {
                    flyingback = true;
                    sr.enabled = true;
                    frame = 0;
                }
                frame += 1 / timeBetweenFrames * Time.deltaTime;
                if (frame > land_textures.Length - 1 && flyingback) {
                    flyingback = false;
                }
                frame %= land_textures.Length;
                sr.sprite = land_textures[(int)frame];
            }
        } else {
            if (!flyingaway) {
                audio.Play();
                flyingaway = true;
                frame = 0;
            }
            frame += 1 / timeBetweenFrames * Time.deltaTime;
            if (frame > fly_textures.Length - 1 && flyingaway) {
                flyingaway = false;
                sr.enabled = false;
            }
            frame %= fly_textures.Length;
            sr.sprite = fly_textures[(int)frame];
        }
    }
}
