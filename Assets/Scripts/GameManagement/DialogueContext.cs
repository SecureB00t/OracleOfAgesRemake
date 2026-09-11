using UnityEngine;

//The context of everything we might need to have a reference to when executing a dialogue action. This can be initialized by DialogueHandler
public class DialogueContext
{
    public NPCController speakingNPC;
    //public PlayerInputController playerInputController;
    public DialogueHandler dialogueHandler;


    //Can expand with more references later. What was that pattern that let's you put an arbitrary number of parameters into a constructor? Builder pattern? Google this. 
    public DialogueContext(NPCController speakingNPC, DialogueHandler dialogueHandler)
    {
        this.speakingNPC = speakingNPC;
        this.dialogueHandler = dialogueHandler;
    }
}
