using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 10.4f;
    [SerializeField] private float jumpForce = 10f;

    //isTouchingGround components
    [SerializeField] private float grndCheckRadius = 0.24f;
    [SerializeField] private Transform grndCheckPos;
    [SerializeField] private LayerMask whatIsGrnd;

    private Rigidbody2D rigidbody2d;
    private bool isTouchingGround = false; //see if can work without initi




    //Animation Variable
    Animator anim;

    //Flip Method Variable
    bool isFacingRight;

    private void Start(){

        //Player's rigidbody
        rigidbody2d = transform.GetComponent<Rigidbody2D>();

        //Assigning Animatior value to anim variable
        anim = GetComponent<Animator>();

        isFacingRight = true;

    }

    void Update() 
    {
        Animation();
        Flip();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //2D Platformer so only need to get the Horizontal Input
        
        isTouchingGround = TouchingGround();
        float horiz = Input.GetAxis("Horizontal");
        rigidbody2d.velocity = new Vector2(horiz * speed, rigidbody2d.velocity.y);

        //Jump (Bool is faster to compute, so compare a bool arg first before comparison)
        if (TouchingGround() && Input.GetAxis("Jump") > 0)
        {
            rigidbody2d.AddForce(new Vector2(0.0f, jumpForce));
            isTouchingGround = false;
        }

    }


        private bool TouchingGround(){
            return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsGrnd);
            

        }


    void Animation()
    {
        //Set the animSpeed variable
        float horiz = Input.GetAxis("Horizontal");
        anim.SetFloat("animSpeed", Math.Abs(horiz));

        //Set the animSpeed variable
        float vert = Input.GetAxis("Vertical");
        anim.SetFloat("vertAnimSpeed", Math.Abs(vert));
    }

    void Flip()
    {
        float horiz = Input.GetAxis("Horizontal");

        //If player is starting to move left but is facing right |or| if player is starting to move right but is facing left
        if (horiz < 0 && isFacingRight == true || horiz > 0 && isFacingRight == false)  
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            isFacingRight = !isFacingRight;
            
        }
    }

}





        //OLD CODE
        // Vector3 pos = transform.position;

        // if (Input.GetKey("w")) 
        // {
        //     //pos.y += speed * Time.deltaTime;
        // }
        // if (Input.GetKey("s")) 
        // {
        //     //pos.y -= speed * Time.deltaTime;
        // }
        // if (Input.GetKey("d")) 
        // {
        //     pos.x += speed * Time.deltaTime;
        // }
        // if (Input.GetKey("a")) 
        // {
        //     pos.x -= speed * Time.deltaTime;
        // }