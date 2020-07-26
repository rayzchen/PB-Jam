using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Transform spots;

    private List<Transform> moveSpots;
    private int randomSpots;
    private Vector2 velocity = Vector2.zero;

    void Start() {
        moveSpots = new List<Transform>();
        foreach (Transform child in spots) {
            moveSpots.Add(child);
        }
        waitTime = startWaitTime;
        randomSpots = Random.Range(0, moveSpots.Count);
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector2.SmoothDamp(transform.position, moveSpots[randomSpots].position, ref velocity, speed / 5);

        if(Vector2.Distance(transform.position, moveSpots[randomSpots].position) <= 0.2f) {
            if(waitTime <= 0) {
                randomSpots = Random.Range(0, moveSpots.Count);
                waitTime = startWaitTime;
            } else {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
