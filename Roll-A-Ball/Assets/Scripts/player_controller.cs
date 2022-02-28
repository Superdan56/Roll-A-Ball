using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class player_controller : MonoBehaviour {

    public float speed = 0;
    public TextMeshProUGUI CountText;
    public GameObject winTextObject;
    public float movementX;
    public float movementY;
    public Rigidbody rb;
    private int count;

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue value) {
        Vector2 v = value.Get<Vector2>();  
        movementX = v.x;
    	movementY = v.y;
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce (movement * speed);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {

            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();

        }
    }

    void SetCountText() {
        CountText.text = "Count: " + count.ToString();
        if (count >= 5) {
            winTextObject.SetActive(true);
        }
    }
}