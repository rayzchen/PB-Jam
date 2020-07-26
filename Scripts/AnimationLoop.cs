using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLoop : MonoBehaviour {

    public Sprite[] textures;
    public float timeBetweenFrames;

    float frame;
    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        frame += 1 / timeBetweenFrames * Time.deltaTime;
        frame %= textures.Length;
        sr.sprite = textures[(int)frame];
    }
}
