using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private BoxCollider2D swordCollider;

    private Vector2 input;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    private Coroutine flipCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        // Automatically find the sword collider
        swordCollider = transform.Find("Weapon/Sword").GetComponent<BoxCollider2D>();
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
        Debug.Log(input.y);


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

    private void OnDrawGizmos()
    {
        if (swordCollider == null)
            return;

        Gizmos.color = swordCollider.enabled ? Color.red : Color.gray;

        Vector3 center = swordCollider.transform.TransformPoint(swordCollider.offset);
        Vector3 size = Vector3.Scale(
            swordCollider.size,
            swordCollider.transform.lossyScale
        );

        Gizmos.DrawWireCube(center, size);
    }
}