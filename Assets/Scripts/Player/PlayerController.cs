using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private BoxCollider2D swordCollider;
    [SerializeField] private int health = 6;
    [SerializeField] private Inventory inventory;  
    [SerializeField] private Tool mainTool;

    private Vector2 input;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock block;
    Color[] palette;
//Initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        swordCollider = transform.Find("Weapon/Sword/Hitbox").GetComponent<BoxCollider2D>();

        //NOT CURRENTLY USED
        palette = SpritePaletteProcessor.GetPalette(spriteRenderer.sprite.texture);
        block = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(block);

        block.SetColor("_Palette0", palette[0]);
        block.SetColor("_Palette1", palette[1]);
        Debug.Log(palette[1]);
        block.SetColor("_Palette2", palette[2]);
        block.SetFloat("_DamageFlash", 0f);
        spriteRenderer.SetPropertyBlock(block);
        mainTool.Initialize(inventory);
        

    }
//Movement and Input


    public void MainTool(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mainTool.Use();
        }

    }


//Collision and Damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision);
            Debug.Log("Player collided with enemy: " + collision.gameObject.name);
            Knockback(collision);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            Pickup pickup = collision.GetComponent<Pickup>();

            if (pickup != null)
            {
                inventory.AddAmmo(pickup.itemData, pickup.ammoAmount);

                Debug.Log("OK");
            }

             Destroy(collision.gameObject);
        }
    }

    private void Knockback(Collision2D collision)
    {
        Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
        rb.AddForce(knockbackDirection * 5f, ForceMode2D.Impulse);
    }


    private void TakeDamage(Collision2D collision){
        health -= collision.gameObject.GetComponent<EnemyController>().damage;
        StartCoroutine(DamageFlash());
        Debug.Log("Player took damage! Current health: " + health);
        if (health <= 0){
            Die();
        }
    }

    private void Die(){
        Destroy(gameObject);
    }

    private IEnumerator DamageFlash()
    {

        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Damage flash iteration: " + i);
            spriteRenderer.GetPropertyBlock(block);
            yield return new WaitForSeconds(0.066f);
            block.SetFloat("_DamageFlash", 1f);
            spriteRenderer.SetPropertyBlock(block);
            yield return new WaitForSeconds(0.066f);
            block.SetFloat("_DamageFlash", 0f);
            spriteRenderer.SetPropertyBlock(block);
            Debug.Log("Damage flash reset iteration: " + i);
        }

    }
}