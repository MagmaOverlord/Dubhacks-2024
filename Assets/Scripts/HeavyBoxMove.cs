using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeavyBoxMove : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    public GameObject player1;
    public GameObject player2;

    private int playerCollidingCount = 0;
    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            playerCollidingCount++;
            if (playerCollidingCount >= 2) {
                rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
         if (collider.gameObject.CompareTag("Player")) {
            playerCollidingCount--;
            if (playerCollidingCount < 2) {
                rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
         }
    }
}
