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
    public LayerMask groundLayers;
    public float jumpForce = 0;
    public SphereCollider col;

    void Start() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue value) {
        Vector2 v = value.Get<Vector2>();  
        movementX = v.x;
    	movementY = v.y;
    }

    void OnJump(InputValue value) {
        if (IsGrounded()) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
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

    private bool IsGrounded() {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);
    }
}