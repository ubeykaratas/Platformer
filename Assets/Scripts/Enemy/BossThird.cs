using UnityEngine;

public class BossThird : MonoBehaviour
{
    [SerializeField] private float minSpeed = 1.0f;
    [SerializeField] private float maxSpeed = 1.0f;
    [SerializeField] private float speedIncreaseRate = .1f;
    [SerializeField] private Vector2 areaSize = new Vector2(10f, 10f);

    private Vector2 dir;
    private float speed;
    private Vector2 areaMin;
    private Vector2 areaMax;

    private void Start()
    {
        SetRandomDirection();
        speed = Random.Range(minSpeed, maxSpeed);

        areaMin = new Vector2(-areaSize.x / 2, -areaSize.y / 2);
        areaMax = new Vector2(areaSize.x / 2, areaSize.y / 2);
    }

    private void Update()
    {
        Vector2 newPosition = (Vector2)transform.position + dir * speed *Time.deltaTime;

        if(newPosition.x < areaMin.x || newPosition.x > areaMax.x)
        {
            dir.x = -dir.x;
            newPosition.x = Mathf.Clamp(newPosition.x, areaMin.x, areaMax.x);
            //SetRandomDirection();
            IncreaseSpeed();
        }
        if (newPosition.y < areaMin.y || newPosition.y > areaMax.y)
        {
            dir.y = -dir.y;
            newPosition.y = Mathf.Clamp(newPosition.y, areaMin.y, areaMax.y);
            //SetRandomDirection();
            IncreaseSpeed();
        }

        transform.position = newPosition;
    }

    private void SetRandomDirection()
    {
        dir = Random.insideUnitCircle.normalized;
    }

    private void IncreaseSpeed()
    {
        speed = Mathf.Min(speed + speedIncreaseRate, maxSpeed);
    }


}
