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
    private Vector2 lastDirection;
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
    }


    void Update()
    {
        
        myAnimator.SetBool("isMoving", inputController.directionalInput != Vector2.zero);

        if (inputController.directionalInput != Vector2.zero)
        {
            myAnimator.SetFloat("LastHorizontal", inputController.directionalInput.x);
            myAnimator.SetFloat("LastVertical", inputController.directionalInput.y);

            if (Mathf.Abs(inputController.directionalInput.x) > Mathf.Abs(inputController.directionalInput.y))
            {
                lastDirection = new Vector2(Mathf.Sign(inputController.directionalInput.x), 0);
            }
            else if (Mathf.Abs(inputController.directionalInput.y) > Mathf.Abs(inputController.directionalInput.x))
            {
                lastDirection = new Vector2(0, Mathf.Sign(inputController.directionalInput.y));
            }
            else if (inputController.directionalInput.x != 0 && inputController.directionalInput.y != 0)
            {
                if (Vector2.Dot(inputController.directionalInput, lastDirection) < 0)
                {
                    //Debug.Log("Moonwalk Happened");
                    lastDirection = new Vector2(Mathf.Sign(inputController.directionalInput.x), 0);
                }
            }
            
        }
        myAnimator.SetFloat("Horizontal", lastDirection.x);
        myAnimator.SetFloat("Vertical", lastDirection.y);

        if (myAnimator.GetFloat("Horizontal") == 1)
        {
            spriteRenderer.flipX = true;
        }

        else if (myAnimator.GetFloat("Horizontal") == -1)
        {
            spriteRenderer.flipX = false;
        }

        else if (Mathf.Abs(myAnimator.GetFloat("Vertical")) == 1 && flipCoroutine == null)
        {
            flipCoroutine = StartCoroutine(walkAnimationFlipTimerTrue(.1f));
        }

        UpdateWeaponFlip();
    }
    private IEnumerator walkAnimationFlipTimerTrue(float timeToWait)
    {

        while(inputController.directionalInput.y != 0){
            //transform.localScale = new Vector3(-1f, 1f, 1f);
            spriteRenderer.flipX = true;
            yield return new WaitForSeconds(timeToWait);
            if(inputController.directionalInput.y==0){break;}                            
            //transform.localScale = new Vector3(1f, 1f, 1f);
            spriteRenderer.flipX = false;
            yield return new WaitForSeconds(timeToWait);
        }

        if(inputController.directionalInput == Vector2.zero){
            //transform.localScale = new Vector3(1f, 1f, 1f);
            spriteRenderer.flipX = false;
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

    private void UpdateWeaponFlip()
    {
        Transform weapon = transform.Find("Weapon");

        if (inputController.mainTool.name == "foo")
        {
            weapon.localScale = new Vector3(1f, 1f, 1f);
            return;
        }

        Vector3 scale = weapon.localScale;

        scale.x = spriteRenderer.flipX
            ? -Mathf.Abs(scale.x)
            : Mathf.Abs(scale.x);

        weapon.localScale = scale;
    }
}
