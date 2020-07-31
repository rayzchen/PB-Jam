using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    
    public SpriteRenderer sr;
    public Sprite[] textures;
    public float timeBetweenFrames = 0.25f;

    AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator AsyncLoad(int index) {
        DontDestroyOnLoad(sr.gameObject);
        DontDestroyOnLoad(gameObject);
        Destroy(sr.gameObject.GetComponent<AnimationLoop>());
        for (int i = 0; i < textures.Length; i++) {
            sr.sprite = textures[i];
            yield return new WaitForSecondsRealtime(timeBetweenFrames);
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone) yield return null;
        for (int i = textures.Length - 1; i != -1; i--) {
            sr.sprite = textures[i];
            yield return new WaitForSecondsRealtime(timeBetweenFrames);
        }
        Destroy(sr.gameObject);
        Destroy(gameObject);
    }

    void Update() {
        if (Input.GetMouseButtonUp(0)) {
            audioSource.Play();
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void Level(int level) {
        StartCoroutine(AsyncLoad(level));
    }
}
