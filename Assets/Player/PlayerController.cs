
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : Singleton<PlayerController>, PlayerInputActions.IPlayerActions
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool canMoveVertical = false;


    [SerializeField] private Collider playerMoveBounds;

    [NonSerialized]public PlayerFuelComponent FuelComponent;
    private Rigidbody rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        FuelComponent = GetComponent<PlayerFuelComponent>();
        rb = GetComponent<Rigidbody>();
        PlayerInputManager.PlayerInput.Player.SetCallbacks(this);
        PlayerInputManager.PlayerInput.Player.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!FuelComponent.HasFuel)
        {
            rb.velocity=(Vector3.zero);
            return;
        }
            
        
        Vector2 moveInput = PlayerInputManager.PlayerInput.Player.Move.ReadValue<Vector2>();
        Vector3 finalMoveVector= new Vector3(moveInput.x, 0, moveInput.y);
        
        if (!canMoveVertical)
            finalMoveVector.z=0;
        
        if (!playerMoveBounds.bounds.Contains(rb.position + finalMoveVector))
        {
            rb.velocity=(Vector3.zero);
            return;
        }
        finalMoveVector *= moveSpeed;
        rb.velocity=(finalMoveVector);
    }

    public void OnMove(InputAction.CallbackContext context) {}

    public void OnFire(InputAction.CallbackContext context)
    {
    }
}
