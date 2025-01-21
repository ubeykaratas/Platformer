using System.Collections;
using UnityEngine;

public class PlayerDash : Base
{
    Player player;

    [Header("Dash")]
    [SerializeField] protected bool canDash = true;
    [SerializeField] private float dashTime = .2f;
    [SerializeField] private float dashCooldown = 1.0f;
    [SerializeField] private UnityEngine.UI.Button dashButton;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public override void Start()
    {
        dashButton.gameObject.SetActive(true);
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) StartCoroutine(StartDash());
    }

    public void DashButton()
    {
        if (canDash)
        {
            StartCoroutine(StartDash());
        }
    }

    private IEnumerator StartDash()
    {
        dashButton.interactable = false;
        canDash = false;
        player.CanMove = false;
        player.Dir = player.Facing; //For idle "DASH" clicks

        player.Rb.constraints = player.Rb.constraints | RigidbodyConstraints2D.FreezePositionY;
        player.Speed *= 2; //Double the speed to dash

        yield return new WaitForSeconds(dashTime);
        player.Speed /= 2; //Get the original speed
        player.Rb.constraints = player.Rb.constraints | RigidbodyConstraints2D.FreezePositionX;
        player.Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        player.Rb.velocity = new Vector2(player.Facing * player.Speed, player.Rb.velocity.y); //Adjust the speed of the player after the dash
        player.CanMove = true;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        dashButton.interactable = true;
    }

}
