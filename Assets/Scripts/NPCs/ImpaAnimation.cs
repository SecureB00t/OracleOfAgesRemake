using UnityEngine;

public class ImpaAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock block;
    private Animator animator;
    Color[] palette;
    [SerializeField] private float initialHorizontal;
    [SerializeField] private float initialVertical;
    private Vector2 lastPosition;
    Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = transform.Find("Visuals/Sprite").GetComponent<SpriteRenderer>();
        block = new MaterialPropertyBlock();
        animator = transform.Find("Visuals/Sprite").GetComponent<Animator>();

        palette = SpritePaletteProcessor.GetPalette(spriteRenderer.sprite.texture);
        spriteRenderer.GetPropertyBlock(block);

        block.SetColor("_Palette0", palette[0]);
        block.SetColor("_Palette1", palette[1]);
        block.SetColor("_Palette2", palette[2]);
        block.SetFloat("_Possessed", 0f);
        spriteRenderer.SetPropertyBlock(block);

        //CHANGE BELOW WITH ACTUAL POSSESSION LOGIC
        spriteRenderer.GetPropertyBlock(block);
        block.SetFloat("_Possessed", 1f);
        spriteRenderer.SetPropertyBlock(block);

        animator.SetFloat("Horizontal", initialHorizontal);
        animator.SetFloat("Vertical", initialVertical);
        lastPosition = transform.position;
    }



    

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 movement = currentPosition - lastPosition;

        if(movement != Vector2.zero){
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                // Horizontal movement
                direction = new Vector2(Mathf.Sign(movement.x), 0);
            }
            else if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
            {
                // Vertical movement
                direction = new Vector2(0, Mathf.Sign(movement.y));
            }
            else if (movement.x != 0 && movement.y != 0)
            {
                if (Vector2.Dot(movement, direction) < 0)
                {
                    direction = new Vector2(Mathf.Sign(direction.x), 0);
                }
            }
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }

        lastPosition = currentPosition;

    }
}
