using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MeteorManager : MonoBehaviour
{
    [SerializeField] private Collider playerMoveBounds;
    [SerializeField] private GameObject meteor;
    [SerializeField] private float meteorSpeed = 5f;
    [SerializeField] private Vector2 spawnIntervalRange = new Vector2(5f, 10f);

    private float timeToNextSpawn;
    private float timer;

    private void Start()
    {
        if (playerMoveBounds == null)
        {
            Debug.LogError("Player Move Bounds nie został ustawiony!");
            return;
        }

        ResetSpawnTimer();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToNextSpawn)
        {
            SpawnMeteor();
            ResetSpawnTimer();
        }
        
        
    }

    private void SpawnMeteor()
    {
        Bounds bounds = playerMoveBounds.bounds;
    
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
    
        Vector3 spawnPosition = new Vector3(randomX, 0, 30);
    
        GameObject spawnedMeteor = Instantiate(meteor, spawnPosition, Quaternion.identity);

        Rigidbody rb = spawnedMeteor.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = spawnedMeteor.AddComponent<Rigidbody>();
        }

        // Upewnij się, że ustawiasz właściwy wektor prędkości
        rb.velocity = new Vector3(0, 0, -meteorSpeed);
        
    }

    private void ResetSpawnTimer()
    {
        timer = 0f;
        timeToNextSpawn = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
    }
}   