using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class UniversalManager : MonoBehaviour
{
    [SerializeField] private Collider playerMoveBounds;
    
    [SerializeField] private float defaultObstacleSpeed = 5f;
    [SerializeField] private float enemySpawnInterval = 8f;
    
    [SerializeField] private List<GameObject> obstacles;
    //TODO: add more

    [SerializeField] private List<GameObject> boosts;
    //TODO: add any

    private void Start()
    {
        if (playerMoveBounds == null)
        {
            Debug.LogError("Player bounds???");
            return;
        }
        
        SpawnNextEnemy(obstacles[0]);
    }

    private void SpawnNextEnemy(GameObject kind)
    {
        Bounds bounds = playerMoveBounds.bounds;
        Vector3 spawnPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, 30);

        Instantiate(kind, spawnPosition, new Quaternion(0, 180,0,0));
        Rigidbody rb = kind.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -defaultObstacleSpeed);
        
        if(NextSpawnCooldownRef!=null)
            StopCoroutine(NextSpawnCooldownRef);
        NextSpawnCooldownRef=StartCoroutine(NextSpawnCooldown());
    }

    private GameObject GetNextEnemyType()
    {
        //TODO: change probability depending on "zone" (distance from earth)

        int positionOnList = Random.Range(0, obstacles.Count);
        return obstacles[positionOnList];
    }

    private Coroutine NextSpawnCooldownRef;
    private IEnumerator NextSpawnCooldown()
    {
        yield return new WaitForSecondsRealtime(enemySpawnInterval + Random.Range(0,7f));
        SpawnNextEnemy(GetNextEnemyType());
    }
}