using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{

    public Vector2 directionalInput;

    public void Move(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<Vector2>();
    }
}
