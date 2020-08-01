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
            StartCoroutine(Reveal());
        }
    }

    IEnumerator Reveal() {
        float start = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(start);
        player.pickedUp.Add(itemName);
        GetComponent<IsometricSorter>().sortOffset = 1000;
        float wait = 0f;
        while (wait < 2 - start) {
            transform.position = Vector2.SmoothDamp(transform.position, (Vector2)player.transform.position + Vector2.up * 4, ref vel, 0.5f);
            wait += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
