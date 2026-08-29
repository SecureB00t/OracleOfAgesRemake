using UnityEngine;

public class NPCController : MonoBehaviour
{
    public DialogueHandler dialogueHandler;
    [SerializeField] public Dialogue dialogue;
    void Start(){
        dialogueHandler = FindFirstObjectByType<DialogueHandler>();
    }
    public void Speak(){
        dialogueHandler.HandleDialogue(dialogue);
    }
}
