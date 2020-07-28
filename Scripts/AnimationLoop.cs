using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationLoop : MonoBehaviour {

    public Sprite[] textures;
    public float timeBetweenFrames = 1;
    public float frame = 0;
    //don't remove this!
    public float randomness = 0;
    public bool shouldLoop = true;

    SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        frame += (1 + Random.Range(-randomness, randomness)) / timeBetweenFrames * Time.deltaTime;
        if (shouldLoop) {
            frame %= textures.Length;
        } else {
            frame = Mathf.Clamp(frame, 0, textures.Length - 1);
        }
        sr.sprite = textures[(int)frame];
    }
}
