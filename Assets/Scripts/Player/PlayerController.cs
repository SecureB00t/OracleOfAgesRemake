using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //[SerializeField] private BoxCollider2D swordCollider;
    
    [SerializeField] private Inventory inventory;  
    
    

    private Vector2 input;
    private Rigidbody2D rb;
//Initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //swordCollider = transform.Find("Weapon/Sword/Hitbox").GetComponent<BoxCollider2D>();
    }

//Collision and Damage




}