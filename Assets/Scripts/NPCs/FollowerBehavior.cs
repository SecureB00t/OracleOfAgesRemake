using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerBehavior : MonoBehaviour
{
    public Rigidbody2D targetToFollow;
    private PlayerMovement playerMovement;

    Queue<Vector2> targetMovementPositions;
    
    void Start()
    {
        targetMovementPositions = new Queue<Vector2>();
        targetToFollow = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerMovement = targetToFollow.GetComponent<PlayerMovement>();
        StartCoroutine(moveToPlayer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerMovement.deltaPosition != Vector2.zero)
        {
            targetMovementPositions.Enqueue(targetToFollow.transform.position);
        }

        if (targetMovementPositions.Count > 10)
        {
            transform.position = targetMovementPositions.Dequeue();
        }
    }

    private IEnumerator moveToPlayer()
    {
        float movementSpeed = 5f;
        while (transform.position.y != targetToFollow.position.y)
        {
            //playerAnimator.SetFloat("Horizontal", 0f);
            //playerAnimator.SetFloat("Vertical", Mathf.Sign(targetToFollow.position.y - transform.position.y));
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x,targetToFollow.position.y, transform.position.z),
                movementSpeed * Time.deltaTime
            );

            yield return null;
        }

        while (transform.position.x != targetToFollow.position.x)
        {
            //playerAnimator.SetFloat("Vertical", 0f);
           // playerAnimator.SetFloat("Horizontal", Mathf.Sign(targetToFollow.position.x - transform.position.x));
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(targetToFollow.position.x, transform.position.y, transform.position.z),
                movementSpeed * Time.deltaTime
            );

            yield return null;
        }
    }
}

