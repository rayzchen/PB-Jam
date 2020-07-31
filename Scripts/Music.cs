using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    static bool created = false;

    void Awake() {
        if (!created) {
            DontDestroyOnLoad(gameObject);
            created = true;
        } else {
            Destroy(gameObject);
        }
    }

    void Update() {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            created = false;
            Destroy(gameObject);
        }
    }
}
