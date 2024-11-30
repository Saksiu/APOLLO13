using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DistanceManager : Singleton<DistanceManager>
{
    private const int MoonTotalDistance = 384400000;
    [SerializeField] private int _distanceTraveled;
    [SerializeField] private TextMeshProUGUI _meterDistance;

    public string GetDistanceFormatted()=>FormatToDistance(_distanceTraveled);

    public string FormatToDistance(int totalDistance)
    {
        int distanceLeft = MoonTotalDistance - totalDistance;

        int meters = Mathf.FloorToInt(distanceLeft / 1000F);
        int kilometers = Mathf.FloorToInt(distanceLeft - meters * 1000);
        return string.Format("Distance to moon: /n {0:000000}km;{1:000}m", kilometers, meters); //TODO: verify format
    }
    
    protected override void Awake()
    {
        base.Awake();
        if(!IsTheOne()) return;
        //TODO: reset distance somewhen?
    }

    private void OnDestroy()
    {
        if(!IsTheOne()) return;
        //TODO: reset distance somewhen?
    }

    private void HandleHardLevelReload()
    {
        ResetMeter();
    }
    private void ResetMeter()
    {
        _distanceTraveled = 0;
    }
}
