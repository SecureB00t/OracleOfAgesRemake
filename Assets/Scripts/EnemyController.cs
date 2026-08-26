using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected int HP = 1;
    private Coroutine flashCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon") && flashCoroutine == null)
        {
            Flash();
            HP--;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
            Debug.Log("Enemy hit by weapon trigger!");
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy();    
    }

    public virtual void MoveEnemy()
    {
       // Debug.Log("Enemy is moving.");
    }

    public void Flash()
    {
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        Color flashColor = Color.red;

        float flashDuration = 0.1f; // Duration of each flash
        int flashCount = 3; // Number of flashes

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }

        flashCoroutine = null;
    }
}
