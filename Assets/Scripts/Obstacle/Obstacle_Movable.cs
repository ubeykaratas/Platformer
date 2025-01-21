using UnityEngine;

public class Obstacle_Movable : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private float dir = -1;
    private float leftEdge;
    private float rightEdge;

    private Animator anim;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( (transform.position.x > leftEdge && dir == -1) || (transform.position.x < rightEdge && dir == 1))
        {
            transform.position = new Vector2(transform.position.x + dir * speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            dir = -dir;
            if (anim)
            {
                anim.SetFloat("dir", dir);
            }
        }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
