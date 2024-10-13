using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dubcoin : MonoBehaviour
{
    bool stillRunning = true;

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (!stillRunning) {
            transform.position += Vector3.down * 5f;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (stillRunning) {
            if (collider.gameObject.CompareTag("Player")) {
                SendMessageUpwards("gameNextLevel");
                stillRunning = false;
                audio.Play();
            }
        }
        
    }
}
