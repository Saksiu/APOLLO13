using UnityEngine.InputSystem;

/// <summary>
/// We pretty much do this this way to avoid common issues related to 
/// duplicating generated PlayerInputAction instances, or their lifetime being dependent on scene management
/// -Maks
/// </summary>
public class InputManager
{
    public readonly static PlayerInputActions PlayerInput = new();
}