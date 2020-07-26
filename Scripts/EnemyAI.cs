using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public Transform spots;
    public Sprite[] textures;

    private List<Transform> moveSpots;
    private int randomSpots;
    private Vector2 velocity = Vector2.zero;
    private SpriteRenderer sr;
    private float pose;

    void Start() {
        moveSpots = new List<Transform>();
        foreach (Transform child in spots) {
            moveSpots.Add(child);
        }
        waitTime = startWaitTime;
        randomSpots = Random.Range(0, moveSpots.Count);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        Vector2 before = transform.position;
        transform.position = Vector2.SmoothDamp(transform.position, moveSpots[randomSpots].position, ref velocity, speed / 7);
        Vector2 after = transform.position;
        Vector2 movement = (after - before) * 2 / (Time.deltaTime * 3);
        
        pose += movement.magnitude * 2 * Time.deltaTime;
        pose %= 4;
        if (movement.magnitude < 0.2f) {
            pose = 8;
        }
        sr.flipX = movement.x < 0;

        sr.sprite = textures[(int)pose];
        print(pose);

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
