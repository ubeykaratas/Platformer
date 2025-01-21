using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool invincible = false;
    private bool isDead = false;

    //[SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 1.0f;
    private Rigidbody2D rb;

    private void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        invincible = false;
    }

    public void Damage(int damage)
    {
        if(!invincible) currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if (currentHealth <= 0 && !isDead) 
        {
            StartCoroutine(Death());
        }
    }

    private System.Collections.IEnumerator Death()
    {
        //Vector3 orijinalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        rb.simulated = false;
        rb.velocity = new Vector2(0, 0);

        DeathHandler deathHandler = GetComponent<DeathHandler>();
        if(deathHandler != null )
        {
            deathHandler.HandleDeath();
        }
        else
        {
            Debug.LogWarning("Cannot find death handler!");
        }

        isDead = true;

        yield return new WaitForSeconds(respawnDelay);
    }

    #region Getter/Setter

    public float CurrentHealth { get { return currentHealth; } }
    public bool Invincible
    {
        get { return invincible; }
        set { invincible = value; }
    }
    #endregion
}
