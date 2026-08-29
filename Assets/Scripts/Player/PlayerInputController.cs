using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{

    //TO DO: FIND A BETTER PLACE TO HANDLE THE TOOL INITIALIZATION. PLAYERINTERACTIONCONTROLLER AND THIS SCRIPT ARE LINKED IN A VERY UNINTUITVE WAY
    [SerializeField] public Tool mainTool;
    [SerializeField] public PlayerInteractionController interactionController;
    public Vector2 directionalInput;
    public bool stopPlayerMovement = false;

    void Start(){
        mainTool.Initialize(interactionController.inventory);
    }
    public void Move(InputAction.CallbackContext context)
    {
        if(!stopPlayerMovement){
            directionalInput = context.ReadValue<Vector2>();
        }
    }

    public void MainTool(InputAction.CallbackContext context)
    {
        if (context.started && !stopPlayerMovement)
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