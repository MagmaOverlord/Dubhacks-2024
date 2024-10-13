using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask groundLayer;
    //range - limits values of field - usable for camera zoomout :o
    [Range(0, 10f)] [SerializeField] private float speed = 5f;
    [Range(0, 10f)] [SerializeField] private float jumpVel = 5f;    

    float horizontal = 0.0f;
    float lastJumpY = 0f;
    private bool isFacingRight = true;
    bool jump = false, jumpHeld = false;
 
    [Range(0, 5f)] [SerializeField] private float fallLongMult = 0.85f;
    [Range(0, 5f)] [SerializeField] private float fallShortMult = 4f;

    public ContactFilter2D ContactFilter;
    public bool IsGrounded => rigidBody2D.IsTouching(ContactFilter);

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) {
            horizontal = Input.GetAxis("Horizontal") * speed;
            if (IsGrounded && Input.GetButtonDown("Jump"))
                jump = true;
            jumpHeld = !IsGrounded && Input.GetButton("Jump");
            
            if (!IsGrounded)
            {
                //plays the same animation both times to not have issues, kept it this way to make it easy to change if we have better animations
                if (lastJumpY < transform.position.y)
                {
                    GetComponent<Animator>().Play("SheepuffFall");
                }
                else if(lastJumpY > transform.position.y)
                {
                    GetComponent<Animator>().Play("SheepuffFall");
                }
            }
            else if(horizontal.Equals(0))
                GetComponent<Animator>().Play("SheepuffIdle");
            else
                GetComponent<Animator>().Play("SheepuffMove");
            }
    }

    void FixedUpdate()
    {
        float moveFactor = horizontal * Time.fixedDeltaTime;

        // Movement
        rigidBody2D.velocity = new Vector2(moveFactor * 10f, rigidBody2D.velocity.y);

        // Jumping
        if (jump)
        {
            rigidBody2D.velocity = Vector2.up * jumpVel;
            jump = false;
            lastJumpY = transform.position.y;
        }
    
        // Jumping High
        if(jumpHeld && rigidBody2D.velocity.y > 0)
        {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallLongMult - 1) * Time.fixedDeltaTime;
        }
        // Jumping Low
        else if(!jumpHeld && rigidBody2D.velocity.y > 0)
        {
            rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallShortMult - 1) * Time.fixedDeltaTime;
        }

        // Flip the sprite according to movement direction
        if (moveFactor > 0 && !isFacingRight) flipSprite();
        else if(moveFactor < 0 && isFacingRight) flipSprite();
    }

    private void flipSprite() {
        isFacingRight = !isFacingRight;

        Vector3 transformScale = transform.localScale;
        transformScale.x *= -1;
        transform.localScale = transformScale;
    }

    private void PlayerDeath() {
        isDead = true;
        rigidBody2D.velocity = new Vector2(0, 5f);
        boxCollider2D.isTrigger = true;
        GetComponent<Animator>().Play("SheepuffDeath");
    }

    /*void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Trap")) {
            collision.otherRigidbody.AddForce(new Vector2(rigidBody2D.velocity.x / 2, 0));
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x / 2, rigidBody2D.velocity.y);
        }
    }*/

}

