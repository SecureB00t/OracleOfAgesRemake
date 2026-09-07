using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private PlayerInputController inputController;
    private Rigidbody2D rb;
    public Vector2 deltaPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inputController = GetComponent<PlayerInputController>();
    }


    private void FixedUpdate()
    {
        if (inputController.directionalInput != Vector2.zero)
        {
            Vector2 targetPosition = rb.position + inputController.directionalInput * speed * Time.fixedDeltaTime;
            deltaPosition = targetPosition - rb.position;
            rb.MovePosition(targetPosition);

        }
    }

}
