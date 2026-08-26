using UnityEngine;

public class Bomb : Tool
{
    private bool held = false;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform hodlPoint;
    public override void Use()
    {
        if (!held && inventory.GetAmmo(itemData) > 0)
        {
            held = true;
            GameObject bomb = Instantiate(bombPrefab, hodlPoint);
            bomb.transform.localPosition = Vector3.zero;
            inventory.RemoveAmmo(itemData, 1);
        }
        else if (held)
        {
            held = false;
            GameObject bomb = hodlPoint.GetChild(0).gameObject;
            bomb.transform.parent = null;
            //Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            //rb.isKinematic = false;
            //rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }
}
