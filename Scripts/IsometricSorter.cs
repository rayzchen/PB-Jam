using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsometricSorter : MonoBehaviour {

    SpriteRenderer sr;
    public int sortOffset = 0;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        sr.sortingOrder = (int)(transform.position.y * -10) + sortOffset;
    }
}
