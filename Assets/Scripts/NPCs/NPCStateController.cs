using UnityEngine;
using System.Collections;

public class NPCStateController : MonoBehaviour
{
    [SerializeField]
    private NPCState currentState = NPCState.Default;
    private NPCController npcController;
    private FollowerBehavior followerBehavior;
    private Rigidbody2D rb;
    private NPCState stateToChangeTo;

    void Start()
    {
        followerBehavior = GetComponent<FollowerBehavior>();
        npcController = GetComponent<NPCController>();
        rb = GetComponent<Rigidbody2D>();

        EnterState(currentState);
    }

    void Update()
    {
        
    }

    public void ChangeState(NPCState newState)
    {
        if (newState == currentState)
        {
            return;
        }
        ExitState(currentState);
        currentState = newState;
        EnterState(currentState);
    }

    private void EnterState(NPCState state)
    {
        switch (state)
        {
            case NPCState.Default:
                if (npcController != null)
                {
                    npcController.enabled = true;   
                    rb.simulated = true;
                }
                break;
            case NPCState.Following:
                if (followerBehavior != null)
                {
                    followerBehavior.enabled = true;
                    rb.simulated = false;
                }
                break;
        }
    }

    private void ExitState(NPCState state)
    {
        switch (state)
        {
            case NPCState.Default:
                if (npcController != null)
                {
                    npcController.enabled = false;  
                    rb.simulated = false; 
                }
                break;
            case NPCState.Following:
                if (followerBehavior != null)
                {
                    followerBehavior.enabled = false;
                    rb.simulated = true;
                }
                break;
        }
    }
}
