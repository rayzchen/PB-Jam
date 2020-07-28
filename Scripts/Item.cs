using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsometricSorter))]

public class Item : MonoBehaviour {

    public PlayerController player;
    public string itemName;
    public float minDistance = 2f;

    bool added = false;
    Vector2 vel;

    void Update() {
        if (player.searching && !added && Vector2.Distance(player.transform.position, transform.position) < minDistance) {
            added = true;
            player.pickedUp.Add(itemName);
            StartCoroutine(Reveal());
        }
    }

    IEnumerator Reveal() {
        GetComponent<IsometricSorter>().sortOffset = 1000;
        float wait = 0f;
        while (wait < 1f) {
            transform.position = Vector2.SmoothDamp(transform.position, (Vector2)player.transform.position + Vector2.up * 4, ref vel, 0.5f);
            wait += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
