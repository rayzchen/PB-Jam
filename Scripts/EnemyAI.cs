using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public Transform player;
    public float speed;
    public float startWaitTime;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform moveSpotsParent;
    public Sprite[] textures;
    public GameObject caught_canv;
    public PauseManager hideInPause;
    
    float waitTime;
    Transform randomSpots;
    int current = 0;
    SpriteRenderer sr;
    float pose;
    Vector2 movement;
    List<Transform> moveSpots;
    bool caught = false;
    PlayerController playerScript;
    float initRetreatDistance;
    bool started = false;

    IEnumerator Start() {
        sr = GetComponent<SpriteRenderer>();
        moveSpots = new List<Transform>();
        foreach (Transform child in moveSpotsParent) {
            moveSpots.Add(child);
        }
        randomSpots = RandomSpot();
        playerScript = player.gameObject.GetComponent<PlayerController>();
        initRetreatDistance = retreatDistance;
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        started = true;
    }

    Transform RandomSpot() {
        List<Transform> cp = new List<Transform>(moveSpots);
        cp.Remove(moveSpots[current]);
        current = Random.Range(0, cp.Count);
        return cp[current];
    }

    void Update()  {
        if (started) {
            Vector2 start = transform.position;
            if (playerScript.sprinting){
                retreatDistance = initRetreatDistance * 2;
            } else {
                retreatDistance = initRetreatDistance;
            }

            bool check;
            if (sr.flipX) {
                check = transform.position.x < player.position.x;
            } else {
                check = transform.position.x > player.position.x;
            }
            if ((check && Vector2.Distance(start, player.position) < retreatDistance) || caught) {
                RaycastHit2D hit = Physics2D.Raycast(start, (Vector2)(player.position - (Vector3)start).normalized);
                if (hit.collider != null && hit.collider.transform == player) {
                    FollowPlayer();
                } else {
                    Patrol();
                }
            } else {
                Patrol();
            }

            if (Vector2.Distance(start, player.position) < stoppingDistance) {
                playerScript.CancelInvisibility();
            }
            movement = (Vector2)transform.position - start;
            pose += Time.deltaTime * 8;
            pose %= textures.Length - 1;
            if (movement.magnitude < 0.001f) {
                pose = textures.Length - 1;
            } else {
                sr.flipX = movement.x > 0;
            }
            sr.sprite = textures[(int)pose];
        }
    }

    void Patrol() {
        caught = false;
        transform.position = Vector2.MoveTowards(transform.position, randomSpots.position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, randomSpots.position) <= 0.1f) {
            if (waitTime <= 0) {
                randomSpots = RandomSpot();
                waitTime = startWaitTime;
            } else {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void FollowPlayer() {
        caught = true;
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); 
        } else {
            movement = Vector2.zero;
            Time.timeScale = 0;
            caught_canv.SetActive(true);
            started = false;
            StartCoroutine(Restart());
        }
    }

    IEnumerator Restart() {
        yield return new WaitForSecondsRealtime(0.5f);
        hideInPause.Restart();
    }
}
