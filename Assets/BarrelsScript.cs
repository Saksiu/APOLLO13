using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= 0)
        {
            //Destroy(gameObject);
        }
    }

    private void OnCollisionEnter()
    {
        PlayerController.Instance.FuelComponent.AddFuel(10);
        Destroy(gameObject);
    }
}
