using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Vector2 input;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    private Coroutine flipCoroutine;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        
        input = context.ReadValue<Vector2>();
        myAnimator.SetFloat("Horizontal", input.x);
        myAnimator.SetFloat("Vertical", input.y);
        myAnimator.SetBool("isMoving", input != Vector2.zero);

        if (input.x > 0){
            spriteRenderer.flipX = true;
        }
        else if (input.x < 0){
            spriteRenderer.flipX = false;
        }

        if (input.y != 0 && flipCoroutine == null)
        {
            flipCoroutine = StartCoroutine(walkAnimationFlipTimerTrue(.1f));
        }

        if(input != Vector2.zero){
            myAnimator.SetFloat("LastHorizontal", input.x);
            myAnimator.SetFloat("LastVertical", input.y);
        }

    }

    public void MainTool(InputAction.CallbackContext context)
    {
        if (context.started){
            myAnimator.SetTrigger("attack");
            //myAnimator.SetBool("isAttacking", false);
        }
    }

    private IEnumerator walkAnimationFlipTimerTrue(float timeToWait)
    {
        Debug.Log(input.y);
        while(input.y != 0){
            spriteRenderer.flipX = true;
            yield return new WaitForSeconds(timeToWait);
            if(input.y==0){break;}         
            spriteRenderer.flipX = false;
            yield return new WaitForSeconds(timeToWait);
        }

        flipCoroutine = null;

    }

    void Update()
    {
        rb.MovePosition(rb.position + input * (speed * Time.fixedDeltaTime));
    }
}