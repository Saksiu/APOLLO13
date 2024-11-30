using UnityEngine.InputSystem;

/// <summary>
/// We pretty much do this way to avoid common issues related to 
/// duplicating generated PlayerInputAction instances, or their lifetime being dependent on scene management
/// -Maks
/// </summary>
public class PlayerInputManager
{
    public static readonly PlayerInputActions PlayerInput = new();
}