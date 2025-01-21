using System.Collections;
using UnityEngine;

public class PlayerWallSlide : Base
{
    private Player player;

    [Header("Wall Slide")]
    [SerializeField] protected float wallSlideSpeed = -3f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public override void FixedUpdate()
    {
        CheckWallSlide();
    }

    private void CheckWallSlide()
    {
        if (player.IsTouchingWall && !player.IsGrounded && player.Rb.velocity.y < 0)
        {
            player.IsWallSliding = true;
            WallSlide();
        }
        else
        {
            player.IsWallSliding = false;
            if (!player.IsGrounded) { player.CanJump = false; }
        }
    }

    private void WallSlide()
    {
        if(player.IsWallSliding)
        {
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, wallSlideSpeed);
        }
    }

}
