using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimationController animationController;
    [SerializeField] public int health = 6;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationController>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision);
            Knockback(collision);
        }

        
    }

    private void Knockback(Collision2D collision)
    {
        Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
        rb.AddForce(knockbackDirection * 1f, ForceMode2D.Impulse);
    }


    private void TakeDamage(Collision2D collision){
        health -= collision.gameObject.GetComponent<EnemyController>().damage;
        //StartCoroutine(animationController.DamageFlash());
        animationController.PlayDamageFlash();
        Debug.Log("Player took damage! Current health: " + health);
        if (health <= 0){
            Die();
        }
    }

    private void Die(){
        Destroy(gameObject);
    }
}
