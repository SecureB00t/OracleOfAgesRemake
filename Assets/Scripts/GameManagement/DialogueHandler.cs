using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueHandler : MonoBehaviour
{
    public TMP_Text textMeshPro;
    private int currentMessage;
    private Message message = null;
    private PlayerInputController inputController;
    private Coroutine typewriter;
    private float charactersPerSecond = 20;
    public bool isTyping;

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
    public void HandleDialogue(Dialogue dialogue){ //Readability issue here. Please fix when you get around to it
        
        inputController.StopPlayerMovement();

        if (isTyping){
            StopCoroutine(typewriter);
            textMeshPro.maxVisibleCharacters = message.text.Length;
            isTyping = false;
            textMeshPro.text = message.text;
            return;
        }

        if(!dialogueBox.activeSelf){ //Go to start of message if no dialogue is displayed (Bad approach but whatever)
            currentMessage = dialogue.start;
        }

        else{ //Find the next message from the current message (I don't understand lambda functions)
            message = dialogue.messages.Find(m => m.id == currentMessage);
            currentMessage = message.next;
        }

        message = dialogue.messages.Find(m => m.id == currentMessage); //Set the actual current message

        if (currentMessage == -1)
        {
            dialogueBox.SetActive(false);
            inputController.ResumePlayerMovement();
        }


        else if (!isTyping){ //Display next message

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


