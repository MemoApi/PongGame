using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float move;
    public float moveSpeed;

    public bool isPlayer1;

    Rigidbody2D rb;

    

    void Start()
    {       
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        //Inputs are taken for each player
        if (isPlayer1) move = Input.GetAxisRaw("Vertical2");
        else move = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, move * moveSpeed);
    }

}
