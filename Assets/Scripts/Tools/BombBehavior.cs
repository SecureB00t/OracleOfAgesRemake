using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    private BombTool owner;

    public void Instantiate(BombTool owner)
    {
        this.owner = owner;
    }

    public void FinishExplosition()
    {
        owner?.BombDestroyed(this);
        Destroy(gameObject);
    }
}
