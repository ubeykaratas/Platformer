using System.Collections;
using UnityEngine;

public class PlayerWallJump : Base
{
    Player player;
    [SerializeField] private bool isJumpingOff = false;
    [SerializeField] float jumpOffTime = 0.02f;

    [Header("Wall Jump")]
    [SerializeField] Vector2 wallJumpForce = new Vector2(10f, 22f);
    public float wallJumpDir;

    private void Awake()
    {
        player = GetComponent<Player>();
        wallJumpDir = -player.Facing;
    }

    public override void Update()
    {   
        if (isJumpingOff && player.CanMove)
        {
            StartCoroutine(DontMove());
        }
    }

    public override void FixedUpdate()
    {
        CheckWallJump();
    }

    private void CheckWallJump()
    {
        if (player.IsWallSliding && !player.IsGrounded && player.CanJump)
        {
            player.CanJump = false;
            WallJump();
            isJumpingOff = true;
        }
    }

    private IEnumerator DontMove()
    { //The basic idea of the function is to only cut off the input of the player trying to move towards the wall for [0.02 / given -> jumpOffTime] seconds

        /*For the better wall jumping, sets the direction to '-Facing' 
          so that the movement does not go towards the wall for only [given] seconds*/
        player.Dir = -player.Facing;

        //Cuts player input
        player.CanMove = false;

        yield return new WaitForSeconds(jumpOffTime);
        //Returns everything to its original state after [given] seconds
        player.CanMove = true;
        isJumpingOff = false;
    }        

    protected void WallJump()
    {
        player.Rb.AddForce(new Vector2(-player.Facing * wallJumpForce.x, wallJumpForce.y), ForceMode2D.Impulse);
    }

}
