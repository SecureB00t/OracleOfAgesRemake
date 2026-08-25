using UnityEngine;
using System.Collections;


public class PlayerAnimationController : MonoBehaviour
{
    private Animator myAnimator;
    private PlayerMovement playerMovement;
    private Coroutine flipCoroutine;
    private PlayerInputController inputController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        inputController = GetComponent<PlayerInputController>();
    }

    void Update()
    {
        myAnimator.SetFloat("Horizontal", inputController.directionalInput.x);
        myAnimator.SetFloat("Vertical", inputController.directionalInput.y);
        myAnimator.SetBool("isMoving", inputController.directionalInput != Vector2.zero);

        if (inputController.directionalInput != Vector2.zero)
        {
            myAnimator.SetFloat("LastHorizontal", inputController.directionalInput.x);
            myAnimator.SetFloat("LastVertical", inputController.directionalInput.y);
        }

        if (inputController.directionalInput.y != 0 && flipCoroutine == null)
        {
            flipCoroutine = StartCoroutine(walkAnimationFlipTimerTrue(.1f));
        }
        else if (inputController.directionalInput.y == 0 && flipCoroutine != null)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Reset scale when not moving vertically
        }

        if (inputController.directionalInput.x != 0)
        {
            transform.localScale = new Vector3(-Mathf.Sign(inputController.directionalInput.x), 1f, 1f); // Flip sprite based on horizontal input
        }
    }
    private IEnumerator walkAnimationFlipTimerTrue(float timeToWait)
    {

        while(inputController.directionalInput.y != 0){
            transform.localScale = new Vector3(-1f, 1f, 1f);
            Debug.Log("Flipping sprite to -1");
            yield return new WaitForSeconds(timeToWait);
            if(inputController.directionalInput.y==0){break;}                            
            transform.localScale = new Vector3(1f, 1f, 1f);
            Debug.Log("Flipping sprite to 1");
            yield return new WaitForSeconds(timeToWait);
        }

        if(inputController.directionalInput == Vector2.zero){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }



        flipCoroutine = null;

    }
}
