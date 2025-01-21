using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float cooldown = 1.0f;
    private bool canTakeDamage = true;

    [SerializeField] private float slowDownRate = 2.0f;
    [SerializeField] private float slowDownTime = .5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canTakeDamage && collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            Health playerHealth = collision.GetComponent<Health>();

            StartCoroutine(DoDamage(player, playerHealth));
            canTakeDamage = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canTakeDamage = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canTakeDamage && collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            Health playerHealth = collision.GetComponent<Health>();

            StartCoroutine(DoDamage(player, playerHealth));
            StartCoroutine(damageCooldown());
        }
    }
    private System.Collections.IEnumerator DoDamage(Player player, Health health)
    {
        if(damage >= 999) health.Invincible = false;

        health.Damage(damage);
        if(!health.Invincible)
        {
            player.Speed /= slowDownRate;
            yield return new WaitForSeconds(slowDownTime);
            player.Speed *= slowDownRate;
        }
        
    }

    private System.Collections.IEnumerator damageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(cooldown);
        canTakeDamage = true;
    }
}
