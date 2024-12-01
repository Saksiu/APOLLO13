using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoostManager : Singleton<BoostManager>
{
    [SerializeField] private List<BoostWeightThreshold> m_Thresholds = new();
    
    [SerializeField] private Collider playerMoveBounds;
    [SerializeField] private float barrelsSpeed = 3.0f;
    //private float spawnInterval = 3.0f;
    
    
    
    void Start()
    {
        StartCoroutine(SpawnBoostCoroutine());
    }


    private IEnumerator SpawnBoostCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_Thresholds[EnvManager.Instance.currentThreshold].spawnInterval);
            SpawnBoost();
            
        }
    }
    

    private void SpawnBoost()
    {
        Bounds bounds = playerMoveBounds.bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        Vector3 spawnPosition = new Vector3(randomX, 0, 30);
        GameObject randomBoost = GetRandomBoost();
        if(randomBoost == null)
            return;
        GameObject spawnedBarrel = Instantiate(randomBoost, spawnPosition, Quaternion.identity);
        Rigidbody rb = spawnedBarrel.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = spawnedBarrel.AddComponent<Rigidbody>();
        }

        rb.velocity = new Vector3(0, 0, -barrelsSpeed);
    }

    private GameObject GetRandomBoost()
    {
        if (m_Thresholds.Count == 0)
        {
            Debug.LogError("No thresholds set, ABORTING");
            return null;
        }
            
        int weightSum = 0;
        foreach (var threshhold in m_Thresholds[EnvManager.Instance.currentThreshold].boostWeights)
        {
            weightSum += threshhold.weight;
        }

        int chosen = Random.Range(0,weightSum);
        int currentSum = 0;
        foreach (var threshhold in m_Thresholds[EnvManager.Instance.currentThreshold].boostWeights)
        {
            currentSum += threshhold.weight;
            if (currentSum >= chosen)
            {
                return threshhold.prefab;
            }
        }
        return m_Thresholds[EnvManager.Instance.currentThreshold].boostWeights[0].prefab;
    }
}


[Serializable]
public struct BoostWeightThreshold
{
    public float spawnInterval;
    public List<SpawnableBoost> boostWeights;
    
    [Tooltip("Dictated by envmanager, because")]
    public int threshold;
}
[Serializable]
public struct SpawnableBoost
{
    public GameObject prefab;
    public int weight;
}
