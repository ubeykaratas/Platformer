using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public abstract class Character : Base
{

    [SerializeField] private Rigidbody2D rb;
    protected Animator animator;

    [Header("Movement")]
    [SerializeField] private float maxSpeed = 25.0f;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float dir = 0;
    [SerializeField] private float facing = 1.0f; // 1 -> facing right // -1 -> facing left

    [Header("Jump")]
    [SerializeField] private bool canJump = false;
    [SerializeField] private float maxSpeedY = 21.0f;
    [SerializeField] private float jumpForce = 5.5f;
    

    [Header("General Checks")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool canMove = true;

    [Header("World Check")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(.46f, .005f);

    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 wallCheckSize = new Vector2(.01f, .73f);

    #region monos

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public override void Start()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void Update()
    {
        //handle inputs
        worldCheck();
    }

    public override void FixedUpdate()
    {
        //handle physics
        HandleMovement();
        MovementLimits();
    }

    #endregion

    #region Mechanics
    protected void Move()
    {
        //Check if the player is touching a ground. If so add some velocity
        if (isGrounded)
        {
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }
        else if (!isGrounded &&!isTouchingWall)
        {
            rb.AddForce(new Vector2(dir * speed, 0), ForceMode2D.Impulse);
            if (Mathf.Abs(rb.velocity.x) > dir)
            {
                rb.velocity = new Vector2(dir * speed, rb.velocity.y);
            }
        }
    }

    protected void Jump()
    {
        //For Coyote Jump
        if (rb.velocity.y < 0)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, 0f);
        }

        //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    #endregion

    #region SubMechanics
    protected abstract void HandleJump();
    protected virtual void HandleMovement()
    {
        Move();
    }

    private void MovementLimits()
    {
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(dir * maxSpeed, rb.velocity.y);
        }
        if (Mathf.Abs(Rb.velocity.y) > maxSpeedY)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxSpeedY);
            Debug.Log(rb.velocity);
        }
    }

    protected void Flip(float moveDir)
    {
        //Changing rotation of player and correct wall jump direction based on movement
        if ( (facing == -moveDir) && !isWallSliding)
        {
            facing = -facing;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void worldCheck()
    {
        //Creates a point to check if the player collision touch any object that tagged "ground".
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, whatIsGround);
        //Creates a square to check if the player collision touch any object that tagged "wall".
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }

    #endregion

    #region Visual
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
    #endregion

    #region Getter/Setter
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            if(value > maxSpeed)
            {
                speed = maxSpeed;
            }
            else speed = value;
        }
    }

    public float Dir
    {
        get { return dir; }
        set
        {
            if(value <= 1 && value >= -1)
            {
                dir = value;
            }
        }
    }

    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    public bool CanJump
    {
        get { return canJump; }
        set { canJump = value;}
    }

    public float JumpForce 
    { 
        get { return jumpForce; }
        set { jumpForce = value; }
    }
    public Rigidbody2D Rb { get { return rb; }}
    public bool IsWallSliding { get { return isWallSliding; } set { isWallSliding = value; } }
    public bool IsTouchingWall {  get { return isTouchingWall; } }
    public float Facing { get { return facing; } }
    public bool CanMove { get { return canMove; } set {  canMove = value; } }
    #endregion
}
