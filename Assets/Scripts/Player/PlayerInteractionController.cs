using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;  
    [SerializeField] private Animator animationController;

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
        //Vector2 direction = animationController.;
        float rayDistance = 2f;
        //Debug.DrawRay(transform.position,direction * rayDistance,Color.red, .1f);
        Debug.Log("Interact Activated");
    }



}
