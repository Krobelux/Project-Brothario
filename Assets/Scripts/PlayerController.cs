using System.Collections;
using System.Collections.Generic;
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


    private void Start(){

        //Player's rigidbody
        rigidbody2d = transform.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //2D Platformer so only need to get the Horizontal Input
        float horiz = Input.GetAxis("Horizontal");
        isTouchingGround = TouchingGround();

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