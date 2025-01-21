using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletFab;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float bulletYSpawn = .5f;

    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnBullet();
            spawnTimer = spawnInterval;
        }
    }
    private void SpawnBullet()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + bulletYSpawn, transform.position.z);
        GameObject bullet = Instantiate(bulletFab, spawnPos, Quaternion.identity);
        Destroy(bullet, lifeTime);
        bullet.gameObject.SetActive(true);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if(rb != null )
        {
            rb.velocity = new Vector2(0, -bulletSpeed);
        }
    }

    public float SpawnInterval
    {
        get { return spawnInterval; }
        set { spawnInterval = value; }
    }
    public float BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }
}
