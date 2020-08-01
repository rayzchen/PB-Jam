using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject canv;
    public GameObject controls;
    public SpriteRenderer sr;
    public Sprite[] textures;
    public float timeBetweenFrames = 0.25f;

    bool paused = false;
    bool transition = false;

    public IEnumerator AsyncLoad(int index) {
        sr.transform.parent = null;
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        DontDestroyOnLoad(sr.gameObject);
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < textures.Length; i++) {
            sr.sprite = textures[i];
            yield return new WaitForSecondsRealtime(timeBetweenFrames);
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone) yield return null;
        sr.transform.position = Vector3.zero;
        Time.timeScale = 1;
        for (int i = textures.Length - 1; i != -1; i--) {
            sr.sprite = textures[i];
            yield return new WaitForSecondsRealtime(timeBetweenFrames);
        }
        Destroy(sr.gameObject);
        Destroy(gameObject);
    }

    void Start() {
        Time.timeScale = 1;
        UnPause();
    }

    void Update() {
        if (!transition) {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
                if (!paused) {
                    Pause();
                } else {
                    UnPause();
                }
            }
        }
    }

    public void Pause() {
        canv.SetActive(true);
        controls.SetActive(false);
        Time.timeScale = 0;
        paused = true;
    }

    public void UnPause() {
        canv.SetActive(false);
        controls.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void Controls() {
        canv.SetActive(false);
        controls.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void Restart() {
        transition = true;
        StartCoroutine(AsyncLoad(SceneManager.GetActiveScene().buildIndex));
    }

    public void Menu() {
        transition = true;
        StartCoroutine(AsyncLoad(1));
    }
}
