using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueHandler : MonoBehaviour
{
    public TMP_Text textMeshPro;
    private int? currentMessage;
    private Message message = null;
    private PlayerInputController inputController;
    private Coroutine typewriter;
    private float charactersPerSecond = 20;
    public bool isTyping;
    public bool dialogueFinished = true;
    public NPCController speakingNPC;
    private int? dialogueStart; //used so I don't have to manually reset the dialogue start point on reset. Maybe change back to changing the dialogue scriptable object directly for the full release? Save tracking?
    private DialogueContext dialogueContext;

    [SerializeField] public GameObject dialogueBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputController = FindAnyObjectByType<PlayerInputController>();
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
    public void HandleDialogue(Dialogue dialogue, NPCController npc){ //Readability issue here. Please fix when you get around to it. Yeah, now it needs a major refactor
        
        inputController.StopPlayerMovement();
        

        if (dialogueStart == null)
        {
            dialogueStart = dialogue.start;
        }

        if (isTyping){
            StopCoroutine(typewriter);
            textMeshPro.maxVisibleCharacters = message.text.Length;
            isTyping = false;
            textMeshPro.text = message.text;
            return;
        }

        if(dialogueFinished){ //Go to start of message if no dialogue is displayed (Bad approach but whatever)
            dialogueFinished = false;
            speakingNPC = npc;
            currentMessage = dialogueStart;
            dialogueContext = new DialogueContext(speakingNPC, this);
            //currentMessage = dialogue.start;
        }

        else{ //Find the next message from the current message (I don't understand lambda functions)
            message = dialogue.messages.Find(m => m.id == currentMessage);
            currentMessage = message.next;
        }

        message = dialogue.messages.Find(m => m.id == currentMessage); //Set the actual current message


        if (message != null){
            if (message.continuePoint != -1)
            {
                dialogueStart = message.continuePoint;
            }
        }
        if (currentMessage == -1)
        {
            dialogueBox.SetActive(false);
            inputController.ResumePlayerMovement();
            speakingNPC = null;
            dialogueFinished = true;

        }


        else if (!isTyping)
        { //Display next message
            foreach(DialogueAction action in message.actions)
            {
                action.Execute(dialogueContext);
            }
            dialogueBox.SetActive(true);
            typewriter= StartCoroutine(TypewriterEffect(message.text));
        }

    }




    private IEnumerator TypewriterEffect(string line)
    {

        textMeshPro.text = line;
        textMeshPro.ForceMeshUpdate();
        textMeshPro.maxVisibleCharacters = 0;
        float timer = 0;
        int visibleCharacters = 0;
        float interval = 1f/charactersPerSecond;

        while (visibleCharacters < line.Length)
        {
            isTyping = true;
            timer += Time.deltaTime;                            //I think this is clever. Time.deltaTime is synced to real seconds. Not framerate. 
                                                                //We run the timer and when it is greater than our chars per second, we subtract the timing of chars per second to make sure
            if (timer >= interval)                              //we play catchup if needed.
            {                                                   //eg. T=0, i = .1. T=0>T=.5>T=.1> T=0
                timer -= interval;

                visibleCharacters++;
                textMeshPro.maxVisibleCharacters = visibleCharacters;
            }

            yield return null;
        }
        isTyping = false;
    }
}


