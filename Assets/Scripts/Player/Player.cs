using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    [Header("Player Speed")]
    [SerializeField] private float runSpeed = 10.0f;

    [Header("Player Jump")]
    [SerializeField] private float jumpTime = 0.1f;
    [SerializeField] private float jumpTimeCounter;
    [SerializeField] private bool isButtonPressed = false;
    [SerializeField] private bool isJumpButtonPressed = false;
    [SerializeField] private bool releaseJump;
    [SerializeField] private float coyoteTime = 0.07f;
    [SerializeField] private float coyoteTimeCounter;
    [Range(0, 3)][SerializeField] private int jumpClampMult = 2;
    [SerializeField]private bool isJumping;

    #region monos

    public override void Awake()
    {
        base.Awake();
        gameObject.AddComponent<PlayerDeathHandler>();
    }

    public override void Start()
    {
        base.Start();
        Speed = runSpeed;
    }

    public override void Update()
    {
        inputs();
        coyoteTimeCounter = IsGrounded ? coyoteTime : coyoteTimeCounter - Time.deltaTime;

        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        HandleJump();
    }

    protected void inputs()
    {
        //Get horizontal direction
        if(!isButtonPressed && CanMove)
        {
            Dir = Input.GetAxisRaw("Horizontal");
        }

        //Check if the jump key pressed
        if (Input.GetKeyDown(KeyCode.Space)) CanJump = true;
        //Check if the jump key released
        if (Input.GetKeyUp(KeyCode.Space)) releaseJump = true;

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
    #endregion

    #region Movement
    protected override void HandleMovement()
    {
        base.HandleMovement();
        Flip(Dir);
    }
    #endregion

    #region Jump
    protected override void HandleJump()
    {
        if(coyoteTimeCounter > 0 && CanJump)
        {
            CanJump = false;
            jumpTimeCounter = jumpTime;
        }
        if( (Input.GetKey(KeyCode.Space) || (isJumpButtonPressed)) && (jumpTimeCounter > 0) && !IsWallSliding)
        {
            CanJump = false;
            isJumping = true;
            Jump(); //Jumping
            jumpTimeCounter -= Time.deltaTime;
        }
        if(Rb.velocity.y <= 0) isJumping = false;
        if (releaseJump)
        {
            releaseJump = false;
            coyoteTimeCounter = 0f;
            jumpTimeCounter = 0f;
            if (isJumping && Rb.velocity.y > 0) 
            { 
                Rb.velocity = new Vector2(Rb.velocity.x, Rb.velocity.y / jumpClampMult);
                isJumping = false;
            }
        }
    }
    #endregion

    #region SubMechanics

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IllusionaryWall"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    public void OnDeath()
    {

    }
    #endregion

    public void getButtonRelease()
    {
        coyoteTimeCounter = 0f;
        jumpTimeCounter = 0f;
    }

    #region Getter/Setter

    public bool IsButtonPressed
    {
        get { return isButtonPressed; }
        set { isButtonPressed = value; }
    }

    public bool IsJumpButtonPressed
    {
        get { return isJumpButtonPressed; }
        set { isJumpButtonPressed = value; }
    }

    public float CoyoteTimeCounter { get { return coyoteTimeCounter; } }

    #endregion
}

public class PlayerDeathHandler : DeathHandler
{
    public override void HandleDeath()
    {
        respawnTime = 1.0f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
