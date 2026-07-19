using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected int HP = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            HP--;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
            Debug.Log("Enemy hit by weapon trigger!");
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy();    
    }

    public virtual void MoveEnemy()
    {
       // Debug.Log("Enemy is moving.");
    }
}
