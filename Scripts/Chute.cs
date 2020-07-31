using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chute : MonoBehaviour {

    public Transform player;
    public Transform camera;
    public SpriteRenderer sr;
    public Sprite[] textures;
    public float minDistance = 3;
    public float timeBetweenFrames = 0.25f;
    public float timeBetweenTransition = 0.5f;

    PlayerController playerScript;
    bool transition = false;

    void Start() {
        playerScript = player.gameObject.GetComponent<PlayerController>();
    }

    void Update() {
        if (!transition && Vector2.Distance(player.position, transform.position) < minDistance && Input.GetKey(KeyCode.O)) {
            transition = true;
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport() {
        playerScript.enabled = false;
        sr.transform.parent = null;
        transform.parent = null;
        Time.timeScale = 0;
        DontDestroyOnLoad(sr.gameObject);
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < textures.Length; i++) {
            sr.sprite = textures[i];
            yield return new WaitForSecondsRealtime(timeBetweenFrames);
        }
        AsyncOperation asyncLoad;
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
            asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        } else {
            asyncLoad = SceneManager.LoadSceneAsync(1);
        }
        while (!asyncLoad.isDone) yield return null;
        sr.transform.position = Vector2.zero;
        for (int i = textures.Length - 1; i != -1; i--) {
            sr.sprite = textures[i];
            yield return new WaitForSecondsRealtime(timeBetweenFrames);
        }
        Time.timeScale = 1;
        Destroy(sr.gameObject);
        Destroy(gameObject);
    }
}
