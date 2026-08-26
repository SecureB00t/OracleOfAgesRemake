using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    protected Inventory inventory;
    [SerializeField] protected ItemData itemData;
    public void Initialize(Inventory inv)
    {
        inventory = inv;

    }

    public abstract void Use();
}