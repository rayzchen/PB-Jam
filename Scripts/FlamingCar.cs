using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamingCar : MonoBehaviour {

    public GameObject canv;
    public PauseManager hideInPause;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            canv.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart() {
        yield return new WaitForSecondsRealtime(0.5f);
        hideInPause.Restart();
    }
}
