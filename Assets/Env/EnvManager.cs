using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnvManager : Singleton<EnvManager>
{
    [SerializeField] private List<Threshold> Thresholds;
    public int currentThreshold = 0;

    [Tooltip("Only for distance counting purposes, does not affect actual player movement speed")]
    [SerializeField] private float flySpeed = 5.0f;
    public float flownDistance = 0.0f;
    [SerializeField] private float distanceToMoon;
    public float GetDistanceLeft()=> distanceToMoon-flownDistance;
    
    [Header("Backgrounds")]
    [SerializeField] private GameObject backgroundPrefab;

    [SerializeField] private float backgroundLength;
    
    [SerializeField] private Transform backgroundSpawnPoint;
    [SerializeField] private float backgroundSlideSpeed = 1.0f;
    [SerializeField] private Vector2 backgroundScale = new Vector2(10,1);
    
    [SerializeField] private Vector2 backGroundOffsetMin = new Vector2(0.0f, 0.0f);
    [SerializeField] private Vector2 backGroundOffsetMax = new Vector2(1.0f, 1.0f);
    
    [Tooltip("In seconds")]
    [SerializeField] private float backgroundSpawnInterval = 1.0f;

    private List<GameObject> spawnedBackgrounds=new();

    private void Start()
    {
        //spawn initial background
        Vector3 nextSpawnPoint = backgroundSpawnPoint.position;
        for (int i = 0; i < 20; i++)
        {
            GameObject newBackground = Instantiate(backgroundPrefab);
            newBackground.transform.position = nextSpawnPoint;
            spawnedBackgrounds.Add(newBackground);
            newBackground.GetComponent<MeshRenderer>().material.color = Thresholds[currentThreshold].color;
            newBackground.GetComponent<MeshRenderer>().material.mainTexture=Thresholds[currentThreshold].texture;
            newBackground.GetComponent<MeshRenderer>().material.mainTextureScale=backgroundScale;
            newBackground.GetComponent<MeshRenderer>().material.mainTextureOffset=getRandomBackgroundOffset();
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

    private Vector2 getRandomBackgroundOffset()
    {
        Vector2 toReturn = new Vector2();
        toReturn.x = Random.Range(backGroundOffsetMin.x, backGroundOffsetMax.x);
        toReturn.y = Random.Range(backGroundOffsetMin.y, backGroundOffsetMax.y);
        return toReturn;
    }
    private void spawnNextBackground()
    {
        if(spawnedBackgrounds.Count>0)
            Destroy(spawnedBackgrounds[0]);
        GameObject newBackground = Instantiate(backgroundPrefab, backgroundSpawnPoint);
        newBackground.GetComponent<MeshRenderer>().material.color = Thresholds[currentThreshold].color;
        newBackground.GetComponent<MeshRenderer>().material.mainTexture=Thresholds[currentThreshold].texture;
        newBackground.GetComponent<MeshRenderer>().material.mainTextureScale=backgroundScale;
        newBackground.GetComponent<MeshRenderer>().material.mainTextureOffset=getRandomBackgroundOffset();
        spawnedBackgrounds.Add(newBackground);
        
    }

    private void FixedUpdate()
    {
        flownDistance += Time.fixedDeltaTime*flySpeed;
        if(Thresholds.Count>currentThreshold+1&&flownDistance >= Thresholds[currentThreshold+1].distance)
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