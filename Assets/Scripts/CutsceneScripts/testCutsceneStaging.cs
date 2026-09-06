using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class testCutsceneStaging : MonoBehaviour
{
    private PlayerInputController inputController;
    private Animator myAnimator;
    private PlayerAnimationController animationController;
    private Coroutine flipCoroutine;
    private SpriteRenderer spriteRenderer;
    
    public float speed = 5f;

    private Vector3 targetPosition = new Vector3(87.5f, 53.5f, 0f);

    private void Start()
    {
        inputController = FindAnyObjectByType<PlayerInputController>();
        myAnimator = GetComponent<Animator>();
        animationController = GetComponent<PlayerAnimationController>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

    }

    public void testMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StartCoroutine(MoveToPosition());
        }
    }

    private System.Collections.IEnumerator MoveToPosition()
    {
        animationController.enabled = false;
        myAnimator.SetBool("isMoving", true);
        
        while (transform.position.y != targetPosition.y)
        {
            
            //flipCoroutine = StartCoroutine(walkAnimationFlipTimerTrue(.1f));
            myAnimator.SetFloat("Horizontal", 0f);
            myAnimator.SetFloat("Vertical", Mathf.Sign(targetPosition.y - transform.position.y));
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x,targetPosition.y, transform.position.z),
                speed * Time.deltaTime
            );

            yield return null;
        }

        while (transform.position.x != targetPosition.x)
        {
            myAnimator.SetFloat("Vertical", 0f);
            myAnimator.SetFloat("Horizontal", Mathf.Sign(targetPosition.x - transform.position.x));
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetPosition.x, transform.position.y, transform.position.z),
                speed * Time.deltaTime
            );

            yield return null;
        }
        myAnimator.SetBool("isMoving", false);
        animationController.enabled = true;
    }

}