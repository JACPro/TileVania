using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float runSpeed;
    [SerializeField] float jumpStrength;
    [SerializeField] float climbSpeed;
    
    //State
    bool isAlive = true;

    //Cached component references
    Rigidbody2D rb; 
    Animator animator;
    float startingGravity;

    //Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startingGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();

        //if player is on ground
        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Foreground"))) { //if is in contact with foreground tiles
                Jump();
            }
        }

        //if player is touching a ladder
        if (GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            ClimbLadder();
        }
        else
        {
            animator.SetBool("Climbing", false);
            rb.gravityScale = startingGravity;
        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //value between -1 and 1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        bool isRunning = controlThrow != 0;
        FlipPlayer(isRunning);
        animator.SetBool("Running", isRunning);
    }

    private void Jump() {        
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpStrength);
    }

    private void ClimbLadder() {
        rb.gravityScale = 0;
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); //value between -1 and 1
        Vector2 climbVelocity = new Vector2(rb.velocity.x, controlThrow * climbSpeed);

        Debug.Log(controlThrow);

        rb.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
        Debug.Log(playerHasVerticalSpeed);
        animator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void FlipPlayer(bool isRunning) {
        if (isRunning) {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), transform.localScale.y);
        }
    }
}
