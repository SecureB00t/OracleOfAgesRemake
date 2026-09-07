using UnityEngine;

[System.Serializable]
public class Message
{
    public int id;
    [TextArea]
    public string text;
    public int next;
    public int continuePoint = -1; //Gross magic number. This represents whether the end (closing textbox) of one dialogue leads to the start of another dialogue or loop point for next time you talk to the npc
}
