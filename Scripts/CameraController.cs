using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed;
    public Transform plane;

    Camera mainCamera;

    void Start() {
        mainCamera = GetComponent<Camera>();
        plane.localScale = new Vector3((float) Screen.width / (float) Screen.height, 1, 1);
    }

    void Update() {
        Vector3 moveDirection = Vector3.up * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
        transform.position += moveDirection * speed * Time.deltaTime;
        mainCamera.orthographicSize += Input.GetAxis("3rd Dimension") * speed / 30;
    }
}