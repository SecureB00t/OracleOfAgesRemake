using UnityEngine;

public class ImpaAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock block;
    Color[] palette;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = transform.Find("Visuals/Sprite").GetComponent<SpriteRenderer>();
        block = new MaterialPropertyBlock();

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
    }



    

    // Update is called once per frame
    void Update()
    {
        
    }
}
