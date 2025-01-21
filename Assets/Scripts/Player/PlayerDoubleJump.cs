using System.Collections;
using UnityEngine;

public class PlayerDoubleJump : Base
{
    Player player;

    [Header("Double Jump")]
    [SerializeField] private float doubleJumpForce = 16.5f;
    [SerializeField] private bool canDoubleJump;
    private bool handleDoubleJump;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public override void Update()
    { //In fixed update it will be bugged
        CheckDoubleJump();
    }

    private void CheckDoubleJump()
    {
        if (player.IsGrounded || player.IsWallSliding)
        {
            canDoubleJump = true;
        }
        if (player.CanJump && !player.IsGrounded && canDoubleJump && !player.IsWallSliding && player.CoyoteTimeCounter <= 0)
        {
            canDoubleJump = false;
            player.CanJump = false;

            DoubleJump();
        }
    }

    #region DoubleJump
    public void DoubleJump()
    {
        player.Rb.velocity = new Vector2(player.Rb.velocity.x, doubleJumpForce);
    }

    #endregion

    #region Getter/Setter

    public bool CanDoubleJump { get { return canDoubleJump; } set { canDoubleJump = value; } }

    #endregion

}