using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] public Inventory inventory;  
    private Animator animationController;
    [SerializeField] public DialogueHandler dialogueHandler;

    void Start(){
        animationController = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            Pickup pickup = collision.GetComponent<Pickup>();

            if (pickup != null)
            {
                inventory.AddAmmo(pickup.itemData, pickup.ammoAmount);
            }

             Destroy(collision.gameObject);
        }
    }

    public void Interact()
    {
        float rayDistance = .25f;

        Vector2 direction = new Vector2(
            animationController.GetFloat("Horizontal"),
            animationController.GetFloat("Vertical")
            ).normalized;


        float rayOffset = 0.5f;

        Vector2 rayOrigin = (Vector2)transform.position + direction * rayOffset;


        RaycastHit2D hit = Physics2D.Raycast(
            rayOrigin,
            direction,
            rayDistance
        );

        if (hit.collider != null && hit.collider.CompareTag("NPC"))
        {
            dialogueHandler.showText();
        }

        Debug.DrawRay(rayOrigin,direction* rayDistance,Color.red, .1f);
    }





}
