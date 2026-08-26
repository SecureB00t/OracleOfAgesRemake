using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{

    [SerializeField] public Tool mainTool;
    public Vector2 directionalInput;

    public void Move(InputAction.CallbackContext context)
    {
        directionalInput = context.ReadValue<Vector2>();
    }

        public void MainTool(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mainTool.Use();
        }

    }

}