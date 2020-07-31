using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {
    
    public SpriteRenderer sr;
    public Sprite[] textures;
    public float timeBetweenFrames = 0.25f;
    public GameObject[] items;

    IEnumerator Start() {
        for (int i = 0; i < textures.Length; i++) {
            sr.sprite = textures[i];
            yield return new WaitForSeconds(timeBetweenFrames);
        }
    }

    public void Next() {
        items[0].SetActive(false);
        items[1].SetActive(false);
        items[2].SetActive(true);
        items[3].SetActive(true);
    }

    public void Okay() {
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu() {
        foreach (GameObject obj in items) {
            Destroy(obj);
        }
        DontDestroyOnLoad(gameObject);
        for (int i = textures.Length - 2; i != -1; i--) {
            sr.sprite = textures[i];
            yield return new WaitForSeconds(timeBetweenFrames);
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        while (!asyncLoad.isDone) yield return null;
        for (int i = 0; i < textures.Length; i++) {
            sr.sprite = textures[i];
            yield return new WaitForSeconds(timeBetweenFrames);
        }
        Destroy(gameObject);
    }
}
