using UnityEngine;

public class BombTool : Tool
{
    private BombBehavior currentBomb;

    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform hodlPoint;
    public override void Use()
    {
        if (currentBomb == null)
        {
            if (inventory.GetAmmo(itemData) <= 0)
                return;

            GameObject bomb = Instantiate(bombPrefab, hodlPoint);
            bomb.transform.localPosition = Vector3.zero;

            currentBomb = bomb.GetComponent<BombBehavior>();  //set reference to the bomb's behavior script

            currentBomb.Instantiate(this); //set our currentBomb variable so we know that we are holding a bomb.
            inventory.RemoveAmmo(itemData, 1);

        }
        else 
        {
            currentBomb.transform.parent = null; //detach the bomb from the player
            currentBomb = null;
        
            //Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            //rb.isKinematic = false;
            //rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
        
        
    }

    public void BombDestroyed(BombBehavior bomb)
    {
        if (currentBomb == bomb) //if the bomb we're referencing is the one that we are holding
        {
            currentBomb = null; //detach bomb from player
        }
    }
}
