using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpaceShip : MonoBehaviour
{
    [SerializeField] private float enemySpaceShipShootInterval = 0.4f;
    [SerializeField] private float bulletSpeed = 6f;
    [SerializeField] private float enemySpaceShipSpeed = 3f;
    [SerializeField] private GameObject missile;
    [SerializeField] private Transform shootPoint;

    private float shootTimer = 0f;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();


        rb.velocity = new Vector3(0, 0, -enemySpaceShipSpeed);
    }

    void FixedUpdate()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= enemySpaceShipShootInterval)
        {
            ShootMissile();
            shootTimer = 0f;
        }

        if (transform.position.z <= 0)
        {
            //Destroy(gameObject);
            //TODO error przy destroyu
        }
    }

    private void OnCollisionEnter()
    {
        PlayerController.Instance.FuelComponent.RemoveFuel(20);
        Destroy(gameObject);
    }

    private void ShootMissile()
    {
        GameObject newMissile = Instantiate(missile, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = newMissile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = shootPoint.forward * bulletSpeed;
        }
    }
}