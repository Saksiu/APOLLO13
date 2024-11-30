using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceManager : Singleton<DistanceManager>
{
    //[ReadOnlyField] (??)
    [SerializeField] private int _distanceTraveled;
    private const int MoonTotalDistance = 384400000;

    public string GetDistanceFormatted()=>FormatToDistance(_distanceTraveled);

    public string FormatToDistance(int totalDistance)
    {
        if (gameObject.transform.parent.gameObject.name == "DistanceToMoon")
            totalDistance = MoonTotalDistance - totalDistance;

        int meters = Mathf.FloorToInt(totalDistance / 1000F);
        int kilometers = Mathf.FloorToInt(totalDistance - meters * 1000);
        return string.Format("{0:0000}:{1:000}", kilometers, meters); //TODO: verify format
    }
    
    [SerializeField] private TextMeshProUGUI _timerText;
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
