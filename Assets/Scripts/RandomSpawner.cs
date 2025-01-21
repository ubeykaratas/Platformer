using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float firstSpawnTime = 50f;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private string objectLayer;
    private bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnPoints.Length == 0 || !objectToSpawn)
        {
            Debug.LogWarning("Spawn points or object to spawn did not set");
            return;
        }        
    }

    private void Update()
    {
        int.TryParse(timer.text, out int value);
        if (value <= firstSpawnTime && (value % spawnInterval == 0 || value == firstSpawnTime) && canSpawn)
        {
            canSpawn = false;
            SpawnObjectAtRandomPoint();
        }
        else if (value % spawnInterval != 0)
        {
            canSpawn = true;
        }
    }

    private void SpawnObjectAtRandomPoint()
    {
        List<Transform> avaliablePoints = new List<Transform>();

        foreach (Transform point in spawnPoints)
        {
            if (!Physics2D.OverlapPoint(point.position, LayerMask.GetMask(objectLayer)))
            {
                avaliablePoints.Add(point);
            }
        }

        if(avaliablePoints.Count >0)
        {
            int randomIndex = Random.Range(0, avaliablePoints.Count);
            Transform spawnPoint = avaliablePoints[randomIndex];
            GameObject obj = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            obj.SetActive(true);
        }
        
    }

}
