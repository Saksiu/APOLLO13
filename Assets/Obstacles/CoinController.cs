using System;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int coinsGiven = 10;
    [SerializeField] private float moveSpeed = 4.0f;
    private void OnCollisionEnter()
    {
        PlayerController.Instance.AddCoins(coinsGiven);
        Destroy(gameObject);
    }

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -moveSpeed);
    }
}
