using UnityEngine;

[System.Serializable]
public class Message
{
    public int id;
    [TextArea]
    public string text;
    public int next;
}
