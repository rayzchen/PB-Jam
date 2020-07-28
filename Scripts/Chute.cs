using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chute : MonoBehaviour {

    public Transform player;
    public Transform camera;
    public float minDistance = 3;

    void Start() {
        
    }

    void Update() {
        if (Vector2.Distance(player.position, transform.position) < minDistance) {
            if (Input.GetKey(KeyCode.O)) {
                MovePlayer();
            }
        }
    }

    void MovePlayer() {
        
    }
}
