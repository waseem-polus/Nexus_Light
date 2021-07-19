using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Animator animator;
    
    private bool isFacingRight = true;
    private int facingDirection = 1;//1 for right, -1 for left
    private float movementInputDirection;
    private bool isRunning;
    private int jumpsLeft;
    private bool canJump;
    private bool isJumpingUp;
    private bool isFalling;
    private bool isGrounded;
    private bool isOnWall;
    private bool isWallSliding;
    private float jumpTimer;
    private bool isAttemptingToJump;
    private bool isPoundingGround;
    private bool isWallJumping;//TODO:
    private bool isWallScaling;//TODO:
    private bool isAirJumping;//TODO:


    [Header("Movement Properties")]
    public float movementSpeed = 10.0f;
    public float wallSlideSpeed = 5.0f;

    [Space(10)]
    public int numberOfJumps = 2;
    public float jumpForce = 16.0f;
    public float variableJumpHightMuliplier = 0.5f;
    public float jumpTimerSet = 0.15f;

    [Space(10)]
    public float wallJumpForce;
    public Vector2 wallJumpDirection;

    [Space(10)]
    public float wallScaleForce;
    public Vector2 wallScaleDirection;

    [Space(10)]
    public float groundPoundForce;
    public Vector2 groundPoundDirection;

    [Header("Ground & Wall Check")]
    public LayerMask whatIsGround;
    [Space(10)]
    public Transform groundCheck;
    public float groundCheckRadius;
    [Space(10)]
    public Transform wallCheck;
    public float wallCheckLength;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpsLeft = numberOfJumps;

        wallJumpDirection.Normalize();
        wallScaleDirection.Normalize();
        groundPoundDirection.Normalize();
    }

    // Update is called once per frame
    void Update() {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
    }

    void FixedUpdate() {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isOnWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckLength, whatIsGround);
    }

    private void CheckIfWallSliding() {
        if (!isGrounded && isOnWall && isFalling) {
            isWallSliding = true;
        } else {
            isWallSliding = false;
        }
    }

    private void CheckMovementDirection () {
        if (isFacingRight && movementInputDirection < 0) {
            Flip();
        } else if (!isFacingRight && movementInputDirection > 0) {
            Flip();
        }

        if (rigidBody.velocity.x >= 0.2 || rigidBody.velocity.x <= -0.2) {
            isRunning = true;
        } else {
            isRunning = false;
        }

        if (rigidBody.velocity.y >= 0.2) {
            isJumpingUp = true;
            isFalling = false;
        } else if (rigidBody.velocity.y <= -0.2) {
            isJumpingUp = false;
            isFalling = true;
        } else {
            isJumpingUp = false;
            isFalling = false;
        }

        if (!isJumpingUp && isAirJumping) {
            isAirJumping = false;
        }

        if (!isJumpingUp && isWallScaling) {
            isWallScaling = false;
        }

        if (!isJumpingUp && isWallJumping) {
            isWallJumping = false;
        }

        if (isPoundingGround && (isGrounded || isOnWall) ) {
            isPoundingGround = false;
            //TODO:Play pound camera effect here
            Debug.Log("Ground Pounded");
        }
    }

    private void Flip() {
        if (!isWallSliding) {
        isFacingRight = !isFacingRight;
        facingDirection *= -1;

        transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void CheckInput() {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump")) {
            if (!isWallSliding || isGrounded) {//TODO: Check if this holds up still (jumpsLeft > 0 && isOnWall)
                NormalJump();
            } else {
                jumpTimer = jumpTimerSet;
                isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonUp("Jump")) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * variableJumpHightMuliplier);
        }


        if (Input.GetButtonDown("Pound")) {
            if (!isGrounded && !isOnWall) {
                PoundGround();
            }
        }
    }

    private void CheckIfCanJump() {
        if (jumpsLeft >= 1) {
            canJump = true;
        } else {
            canJump = false;
        }
        
        if ((isGrounded && !isJumpingUp) || isOnWall) {
            jumpsLeft = numberOfJumps;
        }
    }
    
    private void CheckJump() {
        if (jumpTimer > 0) {
            //WallJump
            if(!isGrounded && isOnWall && movementInputDirection != 0) {
                WallJump();
            } else if (!isGrounded && isWallSliding && movementInputDirection == 0) {
                WallScale();
            } else if (isGrounded) {
                NormalJump();
            }
        } 
    
        if (isAttemptingToJump) {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void PoundGround() {
        isPoundingGround = true;
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0.0f);
        
        Vector2 forceToAdd = new Vector2(groundPoundForce * groundPoundDirection.x , groundPoundForce * groundPoundDirection.y);
        rigidBody.AddForce(forceToAdd, ForceMode2D.Impulse);
    }

    private void NormalJump() {
        if (canJump && !isWallSliding) {
            if (!isGrounded) {
                jumpsLeft--;
                isAirJumping = true;
            }

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

            jumpTimer = 0;
            isAttemptingToJump = false;
        }
    }

    private void WallScale() {
        if(isWallSliding && movementInputDirection == 0) {

            Debug.Log("Wall Scale!");
            isWallScaling = true;
            isWallSliding = false;

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0.0f);
            Vector2 forceToAdd = new Vector2(wallScaleForce * wallScaleDirection.x, wallScaleForce * wallScaleDirection.y);
            rigidBody.AddForce(forceToAdd, ForceMode2D.Impulse);

            jumpTimer = 0;
            isAttemptingToJump = false;
        }
    }

    private void WallJump() {
        if (isOnWall && movementInputDirection != 0) {
            Debug.Log("Wall Jump!");
            isWallSliding = false;
            isWallJumping = true;
            
            rigidBody.velocity = new Vector2(0.0f, 0.0f);
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rigidBody.AddForce(forceToAdd, ForceMode2D.Impulse);
            
            jumpTimer = 0;
            isAttemptingToJump = false;
        }
    }

    private void ApplyMovement() {
        if (!isWallSliding) {
            rigidBody.velocity = new Vector2(movementSpeed * movementInputDirection, rigidBody.velocity.y);
        }

        if (isWallSliding) {
            if (rigidBody.velocity.y < -wallSlideSpeed) {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void UpdateAnimations() {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumpingUp", isJumpingUp);
        animator.SetBool("isJumpingDown", isFalling);
        animator.SetBool("isWallSliding", isWallSliding);
        animator.SetBool("isWallJumping", isWallJumping);
        animator.SetBool("isWallScaling", isWallScaling);
        animator.SetBool("isAirJumping", isAirJumping);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckLength, wallCheck.position.y, wallCheck.position.z));
    }
}