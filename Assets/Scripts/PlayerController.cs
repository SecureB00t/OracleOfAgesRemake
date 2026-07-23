using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private BoxCollider2D swordCollider;
    [SerializeField] private int health = 6;

    private Vector2 input;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    private Coroutine flipCoroutine;
    private MaterialPropertyBlock block;
    Color[] palette;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        // Automatically find the sword collider
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
        

    }

    public void Move(InputAction.CallbackContext context)
    {
        
        input = context.ReadValue<Vector2>();
        myAnimator.SetFloat("Horizontal", input.x);
        myAnimator.SetFloat("Vertical", input.y);
        myAnimator.SetBool("isMoving", input != Vector2.zero);

        if (input != Vector2.zero)
        {
            myAnimator.SetFloat("LastHorizontal", input.x);
            myAnimator.SetFloat("LastVertical", input.y);
        }

        if (input.y != 0 && flipCoroutine == null)
        {
            flipCoroutine = StartCoroutine(walkAnimationFlipTimerTrue(.1f));
        }
        else if (input.y == 0 && flipCoroutine != null)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Reset scale when not moving vertically
        }

        if (input.x != 0)
        {
            transform.localScale = new Vector3(-Mathf.Sign(input.x), 1f, 1f); // Flip sprite based on horizontal input
        }



    }

    public void MainTool(InputAction.CallbackContext context)
    {
        if (context.started){
            myAnimator.SetTrigger("attack");            
            //myAnimator.SetBool("isAttacking", false);

        }
    }


    private void FixedUpdate()
    {
        if (input != Vector2.zero)
        {
            rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);
        }
    }


    private IEnumerator walkAnimationFlipTimerTrue(float timeToWait)
    {

        while(input.y != 0){
            //spriteRenderer.flipX = true;                      FLIP EVERYTHING
            //yield return new WaitForSeconds(timeToWait);      WAIT
            //if(input.y==0){break;}                            STOP IF NO INPUT
            //spriteRenderer.flipX = false;                     FLIP BACK
            //yield return new WaitForSeconds(timeToWait);      WAIT
            transform.localScale = new Vector3(-1f, 1f, 1f);
            yield return new WaitForSeconds(timeToWait);
            if(input.y==0){break;}                            
            transform.localScale = new Vector3(1f, 1f, 1f);
            yield return new WaitForSeconds(timeToWait);
        }

        if(input == Vector2.zero){
            //spriteRenderer.flipX = false;                     RESET IF NO INPUT
            transform.localScale = new Vector3(1f, 1f, 1f);
        }



        flipCoroutine = null;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(collision);
            Debug.Log("Player collided with enemy: " + collision.gameObject.name);
            Knockback(collision);
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