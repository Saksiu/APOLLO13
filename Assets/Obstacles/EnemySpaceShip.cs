using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShip : MonoBehaviour
{
    
    void FixedUpdate()
    {
        if (transform.position.z <= 0)
        {
            Destroy(gameObject);
        }
        
        
    }

    private void OnCollisionEnter()
    {
        PlayerController.Instance.FuelComponent.RemoveFuel(20);
    }
}
