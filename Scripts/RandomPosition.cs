using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour {

    public Vector2[] positions;

    void Start() {
        transform.position = positions[Random.Range(0, positions.Length)];
    }
}
