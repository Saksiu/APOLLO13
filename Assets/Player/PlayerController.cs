using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool canMoveVertical = false;
    private Rigidbody rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveInput = PlayerInputManager.PlayerInput.Player.Move.ReadValue<Vector2>();
        Vector3 finalMoveVector= new Vector3(moveInput.x, 0, moveInput.y);
        rb.velocity=(finalMoveVector * moveSpeed);
    }

    public void OnMove(InputAction.CallbackContext context) {}

    public void OnFire(InputAction.CallbackContext context)
    {
    }
}
