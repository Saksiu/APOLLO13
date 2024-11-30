using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFuelComponent : MonoBehaviour
{
    [Tooltip("Per second")]
    [SerializeField] private float fuelConsumptionRate = 5.0f;
    
    [SerializeField] private float initialFuel = 100.0f;
    public float CurrentFuel = 100.0f;
    
    public bool HasFuel => CurrentFuel > 0.0f;
    
    private void Start()
    {
        CurrentFuel = initialFuel;
    }

    private void FixedUpdate()
    {
        if(!HasFuel) return;
        CurrentFuel -= (fuelConsumptionRate * Time.fixedDeltaTime);
        if (CurrentFuel < 0.0f)
        {
            OnNoFuelLeft();
        }
    }
    
    private void OnNoFuelLeft()
    {
        Debug.Log("No fuel left");
    }
}
