using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFirst : MonoBehaviour
{
    [SerializeField] private int damage = 999;
    [SerializeField] private float speed;
    private float dir = 1;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GameObject.Find("Main Camera").transform;
        gameObject.AddComponent<BossDeathHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().Damage(damage);
            speed = 0;
        }
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x + dir * speed * Time.deltaTime, cameraTransform.position.y);
    }

    public class BossDeathHandler : DeathHandler
    {
        public override void HandleDeath()
        {
            Message message = GetComponent<Message>();
            message.enabled = true;
            respawnTime = 0.0f;
            Destroy(gameObject);
        }
    }

}
