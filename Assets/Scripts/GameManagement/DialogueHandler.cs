using UnityEngine;
using TMPro;
public class DialogueHandler : MonoBehaviour
{
    public TMP_Text textMeshPro;
    [SerializeField] public GameObject dialogueBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshPro = GetComponentInChildren<TMP_Text>();
        textMeshPro.text = "Hello, World!";
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
