using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationLoop : MonoBehaviour {

    public Sprite[] textures;
    public float timeBetweenFrames = 1;
    public float frame = 0;

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
