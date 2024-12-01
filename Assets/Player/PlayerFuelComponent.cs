using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFuelComponent : MonoBehaviour
{
    [Tooltip("Per second")] [SerializeField]
    private float fuelConsumptionRate = 5.0f;

    [SerializeField] public float initialFuel = 100.0f;
    [SerializeField] public float currentFuel = 100.0f;

    public bool HasFuel => currentFuel > 0.0f;

    public void RemoveFuel(float amount)
    {
        currentFuel -= amount;
        currentFuel = Mathf.Max(0.0f, currentFuel);
        if (currentFuel < 0.0f)
        {
            OnNoFuelLeft();
        }
    }

    public void AddFuel(float amount)
    {
        float check = currentFuel + amount;
        if (check >= initialFuel)
        {
            currentFuel = initialFuel;
        }
        else
        {
            currentFuel = check;
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
        currentFuel = Mathf.Max(0.0f, currentFuel);
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
