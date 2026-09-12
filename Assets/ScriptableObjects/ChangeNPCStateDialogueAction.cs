using UnityEngine;

[CreateAssetMenu(fileName = "ChangeNPCStateDialogueAction", menuName = "Scriptable Objects/ChangeNPCStateDialogueAction")]
public class ChangeNPCStateDialogueAction : DialogueAction
{
    [SerializeField] private NPCState newState;

    public override void Execute(DialogueContext context)
    {
        if (context.speakingNPC != null)
        {   
            NPCStateController stateController = context.speakingNPC.GetComponent<NPCStateController>();
            if (stateController != null)
            {
                stateController.ChangeState(newState);
            }
        }
    }
}
