
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool canMoveVertical = false;

    [SerializeField] private Collider playerMoveBounds;
    private Rigidbody rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerInputManager.PlayerInput.Player.SetCallbacks(this);
        PlayerInputManager.PlayerInput.Player.Enable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveInput = PlayerInputManager.PlayerInput.Player.Move.ReadValue<Vector2>();
        Vector3 finalMoveVector= new Vector3(moveInput.x, 0, moveInput.y);
        finalMoveVector *= moveSpeed;
        if(!playerMoveBounds.bounds.Contains(rb.position+finalMoveVector))
            return;
        
        rb.velocity=(finalMoveVector);
    }

    public void OnMove(InputAction.CallbackContext context) {}

    public void OnFire(InputAction.CallbackContext context)
    {
    }
}
