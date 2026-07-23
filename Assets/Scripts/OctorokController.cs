using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OctorokController : EnemyController
{

    private Coroutine moveCoroutine;
    private Rigidbody2D rb;
    private Animator enemyAnimator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 2f;
    Color[] palette;


    private enum possibleActions { Move, Idle, Shoot };
    
    List<WeightedChoice<possibleActions>> actions;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();

        actions = new List<WeightedChoice<possibleActions>>{
            new WeightedChoice<possibleActions>(possibleActions.Move, 50),
            new WeightedChoice<possibleActions>(possibleActions.Idle, 30),
            new WeightedChoice<possibleActions>(possibleActions.Shoot, 20)
        };

        enemyAnimator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        palette = SpritePaletteProcessor.GetPalette(spriteRenderer.sprite.texture);


        MaterialPropertyBlock block = new MaterialPropertyBlock();

        block.SetColor("_Palette0", palette[0]);
        block.SetColor("_Palette1", palette[1]);
        block.SetColor("_Palette2", palette[2]);

        spriteRenderer.SetPropertyBlock(block);

        //Debugging
    }

    public override void MoveEnemy()
    {
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MakeChoice(speed)); // Change direction every 2 seconds
        }
    }

    private IEnumerator MakeChoice(float timeBetweenChoices)
    {
        
        while (true)
        {
            possibleActions choice = ChooseAction();
            switch (choice)
            {
                case possibleActions.Move:
                    Move();
                    break;
                case possibleActions.Idle:
                    break;
                case possibleActions.Shoot:
                    break;
            }
            yield return new WaitForSeconds(timeBetweenChoices);
        }
        
    }

    private possibleActions ChooseAction()
    {
        int totalWeight = 0;
        foreach (var action in actions)                         // SUM UP THE WEIGHTS OF ALL POSSIBLE ACTTIONS
        {
            totalWeight += action.weight;
        }

        int randomValue = Random.Range(0, totalWeight);         // ROLL FOR ACTION
        int currentWeight = 0;

        foreach (var action in actions)                         // CHECK EACH ACTION. ADD THE WEIGHT OF THE ACTION TO THE CURRENT WEIGHT. IF THE RANDOM VALUE IS LESS THAN THE CURRENT WEIGHT, THAT IS THE ACTION TO CHOOSE
        {                                                       // EG. ROLL 69, FIRST ACTION HAS WEIGHT 50. 69 > 50. CHOOSE NEXT ACTION WITH WEIGHT 30. CURRENT WEIGHT IS NOW 80. 69 < 80. CHOOSE THIS ACTION. 
            currentWeight += action.weight;
            if (randomValue < currentWeight)
            {
                return action.value;
            }
        }

        return possibleActions.Idle;
    }

    private void Move()
    {


        int direction = Random.Range(0, 4); // 0 = up, 1 = down, 2 = left, 3 = right
        Vector2 movement = Vector2.zero;

        switch (direction)
        {
            case 0:
                movement = Vector2.up;
                spriteRenderer.flipY = true; // Flip the sprite vertically when moving up
                spriteRenderer.flipX = false; // Ensure the sprite is not flipped horizontally when moving up
                break;
            case 1:
                movement = Vector2.down;
                spriteRenderer.flipY = false; // Unflip the sprite when moving down
                spriteRenderer.flipX = false; // Ensure the sprite is not flipped horizontally when moving down
                break;
            case 2:
                movement = Vector2.left;
                spriteRenderer.flipX = false; // Flip the sprite horizontally when moving left
                spriteRenderer.flipY = false; // Ensure the sprite is not flipped vertically when moving left
                break;
            case 3:
                movement = Vector2.right;
                spriteRenderer.flipX = true; // Unflip the sprite when moving right
                spriteRenderer.flipY = false; // Ensure the sprite is not flipped vertically when moving right
                break;
        }

        rb.linearVelocity = movement * speed;
        enemyAnimator.SetFloat("Horizontal", Mathf.Abs(movement.x));
        enemyAnimator.SetFloat("Vertical", Mathf.Abs(movement.y));
    }

    private void onDestroy()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
    }
}
