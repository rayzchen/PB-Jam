using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxBG : MonoBehaviour {

    public Vector2 scale = Vector2.one * 0.5f;
    public GameObject cameraRoot;
    Vector3 initPos;
    Vector3 targetPos;

    void Start() {
        initPos = transform.position;
    }

    void Update() {
        targetPos = cameraRoot.transform.position * scale;
        transform.position = initPos + targetPos;
    }
}
