using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseOffset : MonoBehaviour {

    public float scale = 1;
    Vector3 mousePos;
    Vector2 screenSize;
    Vector3 initPos;

    void Start() {
        initPos = transform.position;
    }

    void Update() {
        screenSize = new Vector2(Screen.width, Screen.height);
        mousePos = (Input.mousePosition / screenSize) - Vector2.one * 0.5f;
        transform.position = mousePos * scale + initPos;
    }
}
