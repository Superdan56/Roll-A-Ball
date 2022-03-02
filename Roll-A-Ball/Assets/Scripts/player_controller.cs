using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class player_controller : MonoBehaviour {

    public float speed = 0;
    public GameObject winTextObject;
    private float movementX;
    private float movementY;
    public Rigidbody rb;
    private bool isGrounded;
    public float jumpForce = 0;

    void Start() {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue value) {
        Vector2 v = value.Get<Vector2>();  
        movementX = v.x;
    	movementY = v.y;
    }

    void OnJump(InputValue value) {
        if (isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce (movement * speed);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}