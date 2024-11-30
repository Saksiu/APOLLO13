using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

public class UniversalManager : MonoBehaviour
{
    [SerializeField] private Collider playerMoveBounds;
    
    [SerializeField] private float defaultObstacleSpeed = 5f;
    [SerializeField] private float enemySpawnInterval = 8f;
    
    [SerializeField] private GameObject obstacle1; //meteor
    [SerializeField] private GameObject obstacle2; //ship
    //TODO: add more
    
    [SerializeField] private GameObject boost1; //fuel
    //TODO: add any

    private Dictionary<int, GameObject> obstacles;
    private Dictionary<int, GameObject> boosts;

    private void Start()
    {
        if (playerMoveBounds == null)
        {
            Debug.LogError("Player bounds???");
            return;
        }
        
        obstacles.Add(1,obstacle1);
        obstacles.Add(2,obstacle2);
        
        boosts.Add(1,boost1);
        
        SpawnNextEnemy(obstacle1);
    }

    private void SpawnNextEnemy(GameObject kind)
    {
        Bounds bounds = playerMoveBounds.bounds;
        Vector3 spawnPosition = new Vector3(Random.Range(bounds.min.x, bounds.max.x), 0, 30);

        Instantiate(kind, spawnPosition, new Quaternion(0, 180,0,0));
        
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