using UnityEngine;
using TMPro;
public class DialogueHandler : MonoBehaviour
{
    public TMP_Text textMeshPro;
    private int currentMessage;
    private PlayerInputController inputController;

    [SerializeField] public GameObject dialogueBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputController = FindFirstObjectByType<PlayerInputController>();
        textMeshPro = GetComponentInChildren<TMP_Text>();
        textMeshPro.text = "Hello, World!";
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //UNUSED AS OF NOW
    // public void showText(){
    //     dialogueBox.SetActive(true);
    //     textMeshPro.text = "test displauy"; 
    // }

//I HATE MAGIC NUMBERS. FIND A BETTER WAY ASSHOLE
    public void HandleDialogue(Dialogue dialogue){
        Message message;
        inputController.stopPlayerMovement = true;

        if(!dialogueBox.activeSelf){ //Go to start of message if no dialogue is displayed (Bad approach but whatever)
            currentMessage = dialogue.start;
        }

        else{ //Find the next message from the current message (I don't understand lambda functions)
            message = dialogue.messages.Find(m => m.id == currentMessage);
            currentMessage = message.next;
        }

        message = dialogue.messages.Find(m => m.id == currentMessage); //Set the actual current message

        if (currentMessage == -1) //Stop displaying messages (cringe magic number)
        {
            dialogueBox.SetActive(false);
            inputController.stopPlayerMovement = false;
        }

        else{ //Display next message
            textMeshPro.text = message.text;
            dialogueBox.SetActive(true);
        }
        
    }
}
