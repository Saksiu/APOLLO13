using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    
    [SerializeField] private Collider playerMoveBounds;
    [SerializeField] private GameObject barrels;
    [SerializeField] private float barrelsSpeed = 3f;
    [SerializeField] private Vector2 barrelsSpawnIntervalRange = new Vector2(1f, 10f);
    private float barrelsTimer;
    private float barrelsTimeToNextSpawn;


    
    // Start is called before the first frame update
    void Start()
    {


        ResetMeteorSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {
        barrelsTimer += Time.deltaTime;
        
        if (barrelsTimer >= barrelsTimeToNextSpawn)
        {
            SpawnBarrel();
            ResetMeteorSpawnTimer();
        }
    }
    
    
    private void ResetMeteorSpawnTimer()
    {
        barrelsTimer = 0f;
        barrelsTimeToNextSpawn = Random.Range(barrelsSpawnIntervalRange.x, barrelsSpawnIntervalRange.y);
    }

    private void SpawnBarrel()
    {
        Bounds bounds = playerMoveBounds.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        Vector3 spawnPosition = new Vector3(randomX, 0, 30);

        GameObject spawnedBarrel = Instantiate(barrels, spawnPosition, Quaternion.identity);
        Rigidbody rb = spawnedBarrel.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = spawnedBarrel.AddComponent<Rigidbody>();
        }

        rb.velocity = new Vector3(0, 0, -barrelsSpeed);
    }
}
