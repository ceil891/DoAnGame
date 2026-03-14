using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 6f;
    public float doubleJumpForce = 10f;
    public int maxJumpCount = 2;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheck;
    public float wallCheckDistance = 0.15f;
    public LayerMask wallLayer;

    [Header("Wall Jump")]
    public float wallSlideSpeed = 2f;
    public float wallJumpForceX = 6f;
    public float wallJumpForceY = 10f;

    [Header("Visual")]
    public float baseScaleX = 1f;
    public float baseScaleY = 1f;

    [Header("Audio")]
    public AudioSource loopSource;   // Run + WallSlide
    public AudioSource sfxSource;    // Jump, DoubleJump, Land, WallJump
    public AudioClip runClip;
    public AudioClip jumpClip;
    public AudioClip doubleJumpClip;
    public AudioClip landClip;
    public AudioClip wallSlideClip;
    public AudioClip wallJumpClip;

    private Rigidbody2D rb;
    private Animator anim;

    private float moveInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool wasGrounded;
    private bool wasWallSliding;

    private int jumpCount;
    private int facingDir = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.localScale = new Vector3(baseScaleX, baseScaleY, 1);
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal") + MobileInput.move;
        moveInput = Mathf.Clamp(moveInput, -1f, 1f);

        // ===== GROUND =====
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded) jumpCount = 0;

        // ===== WALL =====
        isTouchingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, wallLayer);
        isWallSliding = isTouchingWall && !isGrounded && rb.velocity.y < 0;

        // ===== JUMP INPUT =====
        if (Input.GetKeyDown(KeyCode.Space) || MobileInput.jump)
        {
            if (isWallSliding)
                WallJump();
            else if (isGrounded)
                Jump();
            else if (jumpCount < maxJumpCount - 1)
                DoubleJump();

            MobileInput.jump = false;
        }

        Flip();

        // ===== ANIM =====
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetBool("IsWallSliding", isWallSliding);
        anim.SetFloat("YVelocity", rb.velocity.y);

        // ===== LAND SOUND =====
        if (!wasGrounded && isGrounded)
        {
            sfxSource.PlayOneShot(landClip);
        }

        // ===== WALL SLIDE LOOP =====
        if (isWallSliding && !wasWallSliding)
        {
            loopSource.clip = wallSlideClip;
            loopSource.loop = true;
            loopSource.Play();
        }
        else if (!isWallSliding && wasWallSliding)
        {
            loopSource.Stop();
        }

        // ===== RUN LOOP =====
        if (isGrounded && Mathf.Abs(moveInput) > 0.1f && !isWallSliding)
        {
            if (!loopSource.isPlaying || loopSource.clip != runClip)
            {
                loopSource.clip = runClip;
                loopSource.loop = true;
                loopSource.Play();
            }
        }
        else if (loopSource.clip == runClip)
        {
            loopSource.Stop();
        }

        wasGrounded = isGrounded;
        wasWallSliding = isWallSliding;
    }

    void FixedUpdate()
    {
        if (isWallSliding)
            rb.velocity = new Vector2(0, -wallSlideSpeed);
        else
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        jumpCount = 1;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        sfxSource.PlayOneShot(jumpClip);
    }

    void DoubleJump()
    {
        jumpCount++;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("DoubleJump");
        sfxSource.PlayOneShot(doubleJumpClip);
    }

    void WallJump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-facingDir * wallJumpForceX, wallJumpForceY), ForceMode2D.Impulse);
        facingDir *= -1;
        UpdateScale();
        anim.SetTrigger("WallJump");
        sfxSource.PlayOneShot(wallJumpClip);
    }

    void Flip()
    {
        if (moveInput > 0 && facingDir != 1)
        {
            facingDir = 1;
            UpdateScale();
        }
        else if (moveInput < 0 && facingDir != -1)
        {
            facingDir = -1;
            UpdateScale();
        }
    }

    void UpdateScale()
    {
        transform.localScale = new Vector3(facingDir * baseScaleX, baseScaleY, 1);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(
                wallCheck.position,
                wallCheck.position + Vector3.right * facingDir * wallCheckDistance
            );
        }
    }
}
