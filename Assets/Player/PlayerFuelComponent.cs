using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFuelComponent : MonoBehaviour
{
    [Tooltip("Per second")]
    [SerializeField] private float fuelConsumptionRate = 5.0f;
    
    [SerializeField] public float initialFuel = 100.0f;
    [SerializeField] public float currentFuel = 100.0f;
    
    public bool HasFuel => currentFuel > 0.0f;

    public void RemoveFuel(float amount)
    {
        currentFuel-=amount;
        if (currentFuel < 0.0f)
        {
            OnNoFuelLeft();
        }
    }
    
    private void Start()
    {
        currentFuel = initialFuel;
    }

    private void FixedUpdate()
    {
        if(!HasFuel) return;
        currentFuel -= (fuelConsumptionRate * Time.fixedDeltaTime);
        OnFuelChanged?.Invoke(currentFuel);
        if (currentFuel < 0.0f)
        {
            OnNoFuelLeft();
        }
    }
    
    private void OnNoFuelLeft()
    {
        Debug.Log("No fuel left");
    }
    public static event Action<float> OnFuelChanged;
}
