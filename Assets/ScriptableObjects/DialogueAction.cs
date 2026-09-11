using UnityEngine;

[CreateAssetMenu(fileName = "DialogueAction", menuName = "Scriptable Objects/DialogueAction")]
public abstract class DialogueAction : ScriptableObject
{
    public abstract void Execute(DialogueContext context);
}
