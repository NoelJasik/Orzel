using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public float moveSpeed;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpStopForce;
    public Rigidbody2D rb;
    bool isJumping;
    bool isButtonUp;
    bool canJump;
    [SerializeField]
    float kayooteTime;
    float kayooteTimer;
    bool isGrounded;
    [SerializeField]
    Transform groundChecker;
    [SerializeField]
    float groundCheckRadius;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    bool isTouchingGrass;
    [SerializeField]
    float jumpInputEnabledTime;
    [SerializeField]
    SpriteRenderer sr;
    Animator anim;
    float jumpTimer;
    [SerializeField]
    GameObject wingObject;
        [SerializeField]
    GameObject jumpEffect;
    static public bool hasWings = false;
    // Start is called before the first frame update
    void Start()
    {
        hasWings = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        wingObject.SetActive(hasWings);
        rb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rb.velocity.y);
        if (Input.GetAxis("Horizontal") != 0)
        {
            sr.flipX = rb.velocity.x < 0 ? true : false;
            anim.Play("Walk");
        } else
        {
            anim.Play("Idle");
        }
        
        runJumpChecks();
    }
    void FixedUpdate()
    {
        jump();
    }

    void jump()
    {
        if (isJumping && !isButtonUp && canJump)
        {
            rb.AddForce(jumpForce * transform.up);
            kayooteTimer = 0;
            isJumping = false;
          Instantiate(jumpEffect, groundChecker.position, groundChecker.rotation);
        } else if(isJumping && hasWings && (!isGrounded || !isTouchingGrass))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
             rb.AddForce(jumpForce * transform.up);
            kayooteTimer = 0;
            isJumping = false;
            hasWings = false;
                    Instantiate(jumpEffect, groundChecker.position, groundChecker.rotation);
        }
        if (isButtonUp)
        {
            if (rb.velocity.y >= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / jumpStopForce);
            }
            isButtonUp = false;
            isJumping = false;
        }
    }

    void runJumpChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, whatIsGround);
        //anim.SetFloat("velocityY", rb.velocity.y);
        //anim.SetBool("isGrounded", isGrounded);
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = jumpInputEnabledTime;
        }
        if (isJumping)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (isJumping && jumpTimer <= 0)
        {
            isJumping = false;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isButtonUp = true;
        }
        if(isGrounded && isTouchingGrass)
        {
            kayooteTimer = kayooteTime;
        }
        kayooteTimer -= Time.deltaTime;
        if(kayooteTimer > 0)
        {
            canJump = true;
        } else
        {
            canJump = false;
        }

    }

  public void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform" || other.gameObject.tag == "HotBoy")
        {
              isTouchingGrass = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform" || other.gameObject.tag == "HotBoy")
        {
              isTouchingGrass = true;
        }
    }
     public void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground" || other.gameObject.tag == "Platform" || other.gameObject.tag == "HotBoy")
        {
              isTouchingGrass = true;
        }
    }
}
