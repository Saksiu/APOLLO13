using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private Collider playerMoveBounds;
    [SerializeField] private GameObject meteor;
    [SerializeField] private GameObject enemySpaceShip;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float meteorSpeed = 5f;
    [SerializeField] private Vector2 meteorSpawnIntervalRange = new Vector2(5f, 10f);
    [SerializeField] private Vector2 enemySpaceShipSpawnIntervalRange = new Vector2(8f, 15f);

    private float meteorTimeToNextSpawn;
    private float enemySpaceShipTimeToNextSpawn;

    private float meteorTimer;
    private float enemySpaceShipTimer;

    private void Start()
    {
        if (playerMoveBounds == null)
        {
            Debug.LogError("Player bounds???");
            return;
        }


        ResetMeteorSpawnTimer();
        ResetEnemySpaceShipSpawnTimer();
    }

    private void Update()
    {
        meteorTimer += Time.deltaTime;
        enemySpaceShipTimer += Time.deltaTime;


        if (meteorTimer >= meteorTimeToNextSpawn)
        {
            SpawnMeteor();
            ResetMeteorSpawnTimer();
        }

        if (enemySpaceShipTimer >= enemySpaceShipTimeToNextSpawn)
        {
            SpawnEnemySpaceShip();
            ResetEnemySpaceShipSpawnTimer();
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

        rb.velocity = new Vector3(0, 0, -meteorSpeed);
    }

    private void SpawnEnemySpaceShip()
    {
        Bounds bounds = playerMoveBounds.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        Vector3 spawnPosition = new Vector3(randomX, 0, 30);

        Instantiate(enemySpaceShip, spawnPosition, new Quaternion(0, 180,0,0));
        
    }

    private void ResetMeteorSpawnTimer()
    {
        meteorTimer = 0f;
        meteorTimeToNextSpawn = Random.Range(meteorSpawnIntervalRange.x, meteorSpawnIntervalRange.y);
    }

    private void ResetEnemySpaceShipSpawnTimer()
    {
        enemySpaceShipTimer = 0f;
        enemySpaceShipTimeToNextSpawn =
            Random.Range(enemySpaceShipSpawnIntervalRange.x, enemySpaceShipSpawnIntervalRange.y);
    }
}