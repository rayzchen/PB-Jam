using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {

    public GameObject[] lamps;
    public Sprite[] textures;

    bool on;
    SpriteRenderer sr;

    IEnumerator Start() {
        sr = GetComponent<SpriteRenderer>();
        while (true) {
            Toggle();
            yield return new WaitForSeconds(Random.Range(0f, 1f));
        }
    }

    void Toggle() {
        on = !on;
        if (on) {
            sr.sprite = textures[0];
        } else {
            sr.sprite = textures[1];
        }
        lamps[0].SetActive(on);
        lamps[1].SetActive(on);
    }
}
