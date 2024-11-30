using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvManager : Singleton<EnvManager>
{
    [SerializeField] private List<Threshold> Thresholds;
    public int currentThreshold = 0;

    [Tooltip("Only for distance counting purposes, does not affect actual player movement speed")]
    [SerializeField] private float flySpeed = 5.0f;
    public float flownDistance = 0.0f;
    [SerializeField] private float distanceToMoon;
    
    [Header("Backgrounds")]
    [SerializeField] private GameObject backgroundPrefab;

    [SerializeField] private float backgroundLength;
    
    [SerializeField] private Transform backgroundSpawnPoint;
    [SerializeField] private float backgroundSlideSpeed = 1.0f;
    [SerializeField] private Vector2 backgroundScale = new Vector2(1,1);
    
    [Tooltip("In seconds")]
    [SerializeField] private float backgroundSpawnInterval = 1.0f;

    private List<GameObject> spawnedBackgrounds=new();

    private void Start()
    {
        //spawn initial background
        Vector3 nextSpawnPoint = backgroundSpawnPoint.position;
        for (int i = 0; i < 10; i++)
        {
            GameObject newBackground = Instantiate(backgroundPrefab);
            newBackground.transform.position = nextSpawnPoint;
            spawnedBackgrounds.Add(newBackground);
            newBackground.GetComponent<MeshRenderer>().material.color = Thresholds[currentThreshold].color;
            newBackground.GetComponent<MeshRenderer>().material.mainTexture=Thresholds[currentThreshold].texture;
            newBackground.GetComponent<MeshRenderer>().material.mainTextureScale=backgroundScale;
            nextSpawnPoint.z-=backgroundLength;
            
        }
        StartCoroutine(backgroundReplacementCoroutine());
    }
    
    private IEnumerator backgroundReplacementCoroutine()
    {
        while (true)
        {
            spawnNextBackground();
            yield return new WaitForSeconds(backgroundSpawnInterval);
        }
    }

    private void spawnNextBackground()
    {
        if(spawnedBackgrounds.Count>0)
            Destroy(spawnedBackgrounds[0]);
        GameObject newBackground = Instantiate(backgroundPrefab, backgroundSpawnPoint);
        newBackground.GetComponent<MeshRenderer>().material.color = Thresholds[currentThreshold].color;
        newBackground.GetComponent<MeshRenderer>().material.mainTexture=Thresholds[currentThreshold].texture;
        newBackground.GetComponent<MeshRenderer>().material.mainTextureScale=backgroundScale;
        spawnedBackgrounds.Add(newBackground);
    }

    private void FixedUpdate()
    {
        flownDistance += Time.fixedDeltaTime*flySpeed;
        if(Thresholds.Count>=currentThreshold+1&&flownDistance >= Thresholds[currentThreshold+1].distance)
        {
            currentThreshold++;
        }
        if (flownDistance >= distanceToMoon)
        {
            OnMoonReached();
        }
        foreach(GameObject background in spawnedBackgrounds)
        {
            if(!background) continue;
            Vector3 newPos = background.transform.position;
            newPos.z-=backgroundSlideSpeed;
            background.transform.position=newPos;
        }
    }

    private void OnMoonReached()
    {
        
    }
}

[Serializable]
public struct Threshold
{
    public float distance;
    public Texture2D texture;
    public Color color;
}
