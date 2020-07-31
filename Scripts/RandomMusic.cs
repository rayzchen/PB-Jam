using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class RandomMusic : MonoBehaviour {

    public AudioClip[] clips;

    IEnumerator Start() {
        AudioSource audio = GetComponent<AudioSource>();
        while (true) {
            yield return new WaitForSeconds(Random.Range(3f, 4f));
            audio.clip = clips[Random.Range(0, clips.Length)];
            audio.Play();
            yield return new WaitForFixedUpdate();
        }
    }
}
