using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "Scriptable Objects/DialogueObject")]
public class Dialogue : ScriptableObject
{
    public int start;
    public List<Message> messages;
}
