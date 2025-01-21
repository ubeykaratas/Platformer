using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPreFab;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private bool canSpawn = true;

    [SerializeField] private float bulletSpeedBoost = 2f;
    [SerializeField] private float bulletIntervalBoost = .2f;
    [SerializeField] private float objectSpeedBoost = 1f;

    private float spawnTimer;

    private float o_bulletSpeed;
    private float o_bulletInterval;
    private float o_objectSpeed;

    private void Awake()
    {
        BulletSpawner bullet = enemyPreFab.GetComponent<BulletSpawner>();
        Obstacle_Movable obs = enemyPreFab.GetComponent<Obstacle_Movable>();
        if (bullet)
        {
            o_bulletSpeed = bullet.BulletSpeed;
            o_bulletInterval = bullet.SpawnInterval;
        }
        if (obs)
        {
            o_objectSpeed = obs.Speed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        int value = 0;
        int.TryParse(timer.text, out value);
        if (value % spawnInterval == 0 && canSpawn)
        {
            canSpawn = false;
            SpawnEnemy();
        }
        else if (value % spawnInterval != 0)
        {
            canSpawn = true;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPreFab, enemyPreFab.transform.position, Quaternion.identity);
        BulletSpawner bullet = enemy.GetComponent<BulletSpawner>();
        Obstacle_Movable obs = enemy.GetComponent<Obstacle_Movable>();
        if(bullet)
        {
            bullet.BulletSpeed = o_bulletSpeed;
            bullet.SpawnInterval = o_bulletInterval;

            o_bulletSpeed += bulletSpeedBoost;
            o_bulletInterval = o_bulletInterval - bulletIntervalBoost < 0.4f ? o_bulletInterval - bulletIntervalBoost + .1f : o_bulletInterval - bulletIntervalBoost;
        }
        if(obs)
        {
            obs.Speed = o_objectSpeed;
            o_objectSpeed += objectSpeedBoost;
        }
        enemy.SetActive(true);
        
    }

}
