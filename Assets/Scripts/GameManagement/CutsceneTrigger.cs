using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{

    [SerializeField] private int minSequence;
    [SerializeField] private int maxSequence;
    [SerializeField] private int sequenceToSet;
    [SerializeField] private CutsceneController cutsceneController;

    private BoxCollider2D triggerCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.gameState.sequence >= minSequence && GameManager.Instance.gameState.sequence <= maxSequence)
            {
                GameManager.Instance.gameState.sequence = sequenceToSet;
                cutsceneController.SetCutsceneActive();
                cutsceneController.EnteredTrigger();
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        triggerCollider = GetComponent<BoxCollider2D>();
        if (GameManager.Instance.gameState.sequence < minSequence || GameManager.Instance.gameState.sequence > maxSequence)
        {
            triggerCollider.enabled = false;
            Destroy(gameObject);
        }
        else
        {
            triggerCollider.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
