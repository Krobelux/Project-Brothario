using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //========================= Player properties ============================
    [SerializeField] private float walkSpeed = 6f;       //The default speed if you just move the character
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float runSpeed = 6f;      // Update the IsRunning runSpeed reset argument to match this
    private float maxRunSpeed = 10.0f;
    
    private int plyrHealth = 1;     //The amount of hits a player can take before dying
    [SerializeField] int plyrLives = 3;     //Amount of player lives before game over
    [SerializeField] int plyrCoins = 0;     //Amount of coins the player has


    //========================= Layer/GroundCheck properties ============================
    [SerializeField] private float grndCheckRadius = 0.24f;
    [SerializeField] private Transform grndCheckPos;
    [SerializeField] private LayerMask whatIsGrnd;
    [SerializeField] private LayerMask whatIsHzrd;
    [SerializeField] private Transform blockCheckPos;
    [SerializeField] private LayerMask whatIsBlock;
    private bool isTouchingGround = false;
    private bool isTouchingHazard = false;
    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;
    

    [SerializeField] private float deathDelay = 3.0f;

    Animator anim;      //Animation Variable
    bool isFacingRight;     //Flip Method Variable




    //========================= Methods ============================
    private void Start(){

        //Player's rigidbody
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        collider2d = transform.GetComponent<Collider2D>();

        //Assigning Animatior value to anim variable
        anim = GetComponent<Animator>();
        
        isFacingRight = true;

    }

    void Update() 
    {
        Animation();
        Flip();
        IsRunning();
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isTouchingGround = TouchingGround();
        isTouchingHazard = TouchingHazard();
        // float horiz = Input.GetAxis("Horizontal");
        // rigidbody2d.velocity = new Vector2(horiz * walkSpeed, rigidbody2d.velocity.y);

        //Jump (Bool is faster to compute, so compare a bool arg first before comparison)
        // if (TouchingGround() && Input.GetAxis("Jump") > 0)      //Player will jump if they are both touching the ground and pressing the Jump button
        // {
        //     rigidbody2d.AddForce(new Vector2(0.0f, jumpForce));
        //     isTouchingGround = false;

        // }

        if (TouchingHazard() == true)       //rework this to take damage and then when damage is 0 call death
        {
            Debug.Log("Death occurs.");
            StartCoroutine(WaitForAnim());
            rigidbody2d.velocity = new Vector2(0, 0);

            Debug.Log("Collider.enabled = " + collider2d.enabled);
            collider2d.enabled = false;

        }

        
    }


    private bool TouchingGround(){
        return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsGrnd);
            
    }

    
    private bool TouchingHazard(){      //Overlap Component to check if Player overlaps with hazard
        //Debug.Log("is touching hazard");
        return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsHzrd);
            
    }

    private void IsRunning(){
        float horiz = Input.GetAxis("Horizontal");

        //Player run code block
        if (TouchingGround() && Input.GetKey(KeyCode.LeftShift))        //Player will increase speed based on
        {
            if (runSpeed < maxRunSpeed)
            {
                runSpeed += Time.deltaTime * 12;
            }
            rigidbody2d.velocity = new Vector2(horiz * runSpeed, rigidbody2d.velocity.y);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))     
        {
            runSpeed = 6.0f;       //resets the run speed back to basic value | Update this to match the runspeed property at the top
        }
        else
        {
            rigidbody2d.velocity = new Vector2(horiz * walkSpeed, rigidbody2d.velocity.y);
        }



        //Player jump code block
        if (TouchingGround() && Input.GetKey(KeyCode.Space))      //Player will jump if they are both touching the ground and pressing the Jump button
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Horizontal") != 0)        //Check if player is holding LShift and pressing either A or D
            {
                rigidbody2d.AddForce(new Vector2(horiz * runSpeed, jumpForce + (runSpeed / 2)));
                isTouchingGround = false;
            }
            else
            {
                rigidbody2d.AddForce(new Vector2(horiz, jumpForce));
                isTouchingGround = false;
            }
            

        }
    }

    private IEnumerator WaitForAnim(){
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene("TestLevel");  //Make sure level name is consistent with Scene level names
    }

    void Animation()
    {
        //Set the animSpeed variable
        float horiz = Input.GetAxis("Horizontal");
        anim.SetFloat("animSpeed", Math.Abs(horiz));

        //Set the animSpeed variable
        float vert = Input.GetAxis("Vertical");
        anim.SetFloat("vertAnimSpeed", Math.Abs(vert));

        anim.SetBool("isTouchingGround", isTouchingGround);
        anim.SetBool("isTouchingHazard", isTouchingHazard);

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