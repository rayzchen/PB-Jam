using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {

    public PlayerController player;
    public GameObject item;
    public Sprite open_texture;
    public float minDistance = 3f;

    Transform playerT;

    void Start() {
        playerT = player.transform;
    }

    void Update() {
        if (Vector2.Distance(playerT.position, transform.position) < minDistance) {
            if (player.items.Contains("Key")) {
                if (Input.GetKey(KeyCode.O)) {
                    player.items.Remove("Key");
                    GetComponent<SpriteRenderer>().sprite = open_texture;
                    GameObject itemGO = Instantiate(item);
                    itemGO.GetComponent<Item>().player = player;
                    Destroy(this);
                }
            }
        }
    }
}
