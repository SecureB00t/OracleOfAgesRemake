using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{

    [SerializeField] public Tool mainTool;
    [SerializeField] public PlayerInteractionController interactionController;
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
            GameManager.Instance.gameState.sequence++;
            Debug.Log("Sequence: " + GameManager.Instance.gameState.sequence);
        }

    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Button Pressed");
            interactionController.Interact();
        }
    }

}