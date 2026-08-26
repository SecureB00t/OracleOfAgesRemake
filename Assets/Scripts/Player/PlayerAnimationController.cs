using UnityEngine;
using System.Collections;


public class PlayerAnimationController : MonoBehaviour
{
    private Animator myAnimator;
    private PlayerMovement playerMovement;
    private Coroutine flipCoroutine;
    private PlayerInputController inputController;
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock block;
    Color[] palette;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        inputController = GetComponent<PlayerInputController>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        block = new MaterialPropertyBlock();

        palette = SpritePaletteProcessor.GetPalette(spriteRenderer.sprite.texture);
        block = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(block);

        block.SetColor("_Palette0", palette[0]);
        block.SetColor("_Palette1", palette[1]);
        Debug.Log(palette[1]);
        block.SetColor("_Palette2", palette[2]);
        block.SetFloat("_DamageFlash", 0f);
        spriteRenderer.SetPropertyBlock(block);
        //mainTool.Initialize(inventory);
    }

//TODO - Change so that only once place is flipping the sprite. Avoid overriding in multiple places.
//TODO - Switch from flipping the scale of the whole object to just flipping the sprite. I think physics issues are happening with current implementation. Will have to adjust logic to rotate the weapon. Maybe change weapon scale only?
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

        if (inputController.directionalInput.x != 0 && inputController.directionalInput.y == 0)
        {
            transform.localScale = new Vector3(-Mathf.Sign(inputController.directionalInput.x), 1f, 1f); // Flip sprite based on horizontal input
        }
    }
    private IEnumerator walkAnimationFlipTimerTrue(float timeToWait)
    {

        while(inputController.directionalInput.y != 0){
            transform.localScale = new Vector3(-1f, 1f, 1f);
            yield return new WaitForSeconds(timeToWait);
            if(inputController.directionalInput.y==0){break;}                            
            transform.localScale = new Vector3(1f, 1f, 1f);
            yield return new WaitForSeconds(timeToWait);
        }

        if(inputController.directionalInput == Vector2.zero){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }



        flipCoroutine = null;

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

    public void PlayDamageFlash(){
        StartCoroutine(DamageFlash());
    }
}
