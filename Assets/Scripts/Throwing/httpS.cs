using UnityEngine;

public class httpS : MonoBehaviour
{
    [SerializeField] private Transform bossObject;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Dikdörtgen tag'ini kontrol et
        {
            StartCoroutine(JumpUpAndMove());
        }
        if (collision.CompareTag("Boss"))
        {
            Destroy(gameObject);
            collision.GetComponent<Health>().Damage(damage);
        }
    }

    private System.Collections.IEnumerator JumpUpAndMove()
    {
        Vector2 startPos = transform.position;
        float elapsedTime = 0f;

        // Move upward first
        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;
            transform.position = Vector2.Lerp(startPos, startPos + Vector2.up * jumpHeight, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (bossObject)
        {
            Vector2 targetPos = bossObject.position;
            // Reset elapsed time for horizontal movement
            startPos = transform.position;
            elapsedTime = 0f;

            // Move towards the target with an arc
            while (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;

                // Calculate position for the horizontal movement
                float x = Mathf.Lerp(startPos.x, targetPos.x, t);
                float y = Mathf.Lerp(startPos.y, targetPos.y, t);

                // Apply the parabolic arc to the vertical position
                float parabola = Mathf.Sin(t * Mathf.PI); // Sinusoidal arc effect
                transform.position = new Vector2(x, y + (parabola * jumpHeight));

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            Destroy(gameObject);
            yield return null;
        }
        
    }
}