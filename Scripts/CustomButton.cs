using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class CustomButton : MonoBehaviour {

    public UnityEvent callback;

    void OnMouseDown() {
        callback.Invoke();
        Destroy(this);
    }
}
