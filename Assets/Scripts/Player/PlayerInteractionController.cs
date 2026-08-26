using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;  

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

}
