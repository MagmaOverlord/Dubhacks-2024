using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{

    //integer to multiply directional values by (messed up but yknow)
    public float direction = 1f;
    public float pushStrength = 20f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Player")) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction);
            if (hit.collider.gameObject.CompareTag("Player")) {
                hit.collider.attachedRigidbody.AddForce(Vector2.right * pushStrength * direction);
            }
        }
    }
}
