using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public GameObject hideInPause;

    bool paused = false;

    void Start() {
        Time.timeScale = 1;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (!paused) {
                Pause();
            } else {
                UnPause();
            }
        }
    }

    public void Pause() {
        hideInPause.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void UnPause() {
        hideInPause.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        SceneManager.LoadScene("Main Menu");
    }
}
