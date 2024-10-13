using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spike : MonoBehaviour
{
    bool stillRunning = true;
    public GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (stillRunning) {
            if (collision.gameObject.CompareTag("Player")) {
                SendMessageUpwards("gamePlayerDeath");
                stillRunning = false;
            }
        }
    }
}
