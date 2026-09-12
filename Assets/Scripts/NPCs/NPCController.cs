using UnityEngine;

public class NPCController : MonoBehaviour
{
    public DialogueHandler dialogueHandler;
    [SerializeField] public Dialogue dialogue;
    [SerializeField] public int dialogueStartPoint;
    void Start(){
        dialogueHandler = FindAnyObjectByType<DialogueHandler>();
    }
    public void Speak(){
        dialogueHandler.HandleDialogue(dialogue, this);
    }
}
