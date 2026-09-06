using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputController : MonoBehaviour
{

    //TO DO: FIND A BETTER PLACE TO HANDLE THE TOOL INITIALIZATION. PLAYERINTERACTIONCONTROLLER AND THIS SCRIPT ARE LINKED IN A VERY UNINTUITVE WAY
    [SerializeField] public Tool mainTool;
    [SerializeField] public PlayerInteractionController interactionController;
    public Vector2 directionalInput;
    private PlayerMovement playerMovement;
    private bool stopPlayerMovement = false;

    void Start(){
        mainTool.Initialize(interactionController.inventory);
        playerMovement = GetComponent<PlayerMovement>();
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
        }

    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interactionController.Interact();
        }
    }

    public void StopPlayerMovement(){
        stopPlayerMovement = true;
        playerMovement.enabled = false;
        directionalInput = Vector2.zero;
    }

    public void ResumePlayerMovement(){
        directionalInput = Vector2.zero;
        stopPlayerMovement = false;
        playerMovement.enabled = true;

    }

}