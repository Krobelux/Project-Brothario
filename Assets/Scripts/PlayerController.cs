using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //========================= Player properties ============================
    public int plyrHealth = 3;     //The amount of hits a player can take before dying
    //========================= Movement ============================
    [SerializeField] private float walkSpeed = 6f;       //The default speed if you just move the character
    
    [SerializeField] private float runSpeed = 6f;      // Update the IsRunning runSpeed reset argument to match this
    [SerializeField] private float maxRunSpeed = 10.0f;
    //========================= Jump ============================
    [SerializeField] private float jumpForce = 16f;
    public float jumpDelay = 0.25f;     //Jump delay value so the player's jump button can register beyond just 1 frame
    private float jumpTimer;

    //========================= Physics properties ============================
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    //========================= Layer/GroundCheck properties ============================
    [SerializeField] private float grndCheckRadius = 0.24f;
    [SerializeField] private Transform grndCheckPos;
    [SerializeField] private LayerMask whatIsGrnd;
    [SerializeField] private LayerMask whatIsHzrd;
    [SerializeField] private LayerMask whatIsPltfrm;
    [SerializeField] private LayerMask whatIsJumpableEnemy;
    [SerializeField] private LayerMask whatIsDF;
    // [SerializeField] private Transform blockCheckPos;
    [SerializeField] private LayerMask whatIsBlock;
    private bool isTouchingGround = false;
    private bool isTouchingHazard = false;
    private bool isCrouching = false;
    public bool isTouchingMovPlatform = false;
    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;
    private Vector2 direction;
    

    [SerializeField] private float deathDelay = 3.0f;

    Animator anim;      //Animation reference Variable
    bool isFacingRight;     //Flip Method reference Variable




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
        isTouchingGround = TouchingGround();
        isTouchingHazard = TouchingHazard();
        isCrouching = Crouching();
        isTouchingMovPlatform = TouchingMovingPlatform();

        Animation();
        Flip();

        if(Input.GetButton("Jump")){
            jumpTimer = Time.time + jumpDelay;
        }
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        Movement(direction.x);
        // if (direction.x != 0){
        //     gameObject.transform.SetParent(null);
            
        // }
        // else if (TouchingMovingPlatform())
        // {

        // }

        if(jumpTimer > Time.time && TouchingGround() == true){
            Jump();
            isTouchingGround = false;
        }

        if (TouchingHazard() == true)       //Hazards are things like spike balls, spike walls, etc.
        {   
            Debug.Log("is touching Hazard");
        }

        // if (TouchingDeathField() == true)       //Death Field is the zone underneath the levels that immediately kill the player
        // {
        //     Debug.Log("is touching Death Field");
        //     //plyrLives -= 1;

        // }

        modifyPhysics();
    }


    private bool TouchingGround(){
        return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsGrnd);
    }
    private bool TouchingHazard(){      //Overlap Component to check if Player overlaps with hazard
        //Debug.Log("is touching hazard");
        return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsHzrd);       
    }
    private bool JumpOnEnemy(){
        return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsJumpableEnemy);    //Set enemies who Brothario can jump off
    }
    private bool TouchingDeathField(){      //Overlap Component to check if Player overlaps with falling off map
        return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsDF);       
    }
    public bool TouchingMovingPlatform(){      //Overlap Component to check if Player overlaps with a moving platform
        //Debug.Log("is touching moving platform");
        return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsPltfrm);       
    }

    private bool Crouching()
    {
        return Input.GetKey(KeyCode.S);
    }


    void Movement(float horiz){ 

        if (TouchingGround()){
            rigidbody2d.AddForce(Vector2.right * horiz * walkSpeed);    //Vector2.Right = (x:1, y:0) * horiz(-1, 0, or 1 (left, stop, right)) * walking speed
            
        
            if (Input.GetKey(KeyCode.LeftShift) && horiz != 0) 
            {   //Running Code
                rigidbody2d.AddForce(Vector2.right * horiz * runSpeed);
                if (runSpeed < maxRunSpeed)
                {
                    runSpeed += Time.deltaTime * 4;

                }
                
                if (Mathf.Abs(rigidbody2d.velocity.x) > maxRunSpeed)
                {
                    rigidbody2d.velocity = new Vector2(Mathf.Sign(rigidbody2d.velocity.x) * maxRunSpeed, rigidbody2d.velocity.y);
                }

                //Supposed to reset runspeed back to normal values?!?!
                if (Input.GetKeyUp(KeyCode.LeftShift))     
                {
                    runSpeed = walkSpeed;
                }
                
            }
            
            else if (Mathf.Abs(rigidbody2d.velocity.x) > walkSpeed)      
            {   //Controls Player to not move faster than Walk Speed if not running
                rigidbody2d.velocity = new Vector2(Mathf.Sign(rigidbody2d.velocity.x) * walkSpeed, rigidbody2d.velocity.y);
            }



        }
    }

    void Jump(){
        
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
        rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;

    }

    void modifyPhysics(){
        bool changingDirections = (direction.x > 0 && rigidbody2d.velocity.x < 0) || (direction.x < 0 && rigidbody2d.velocity.x > 0);

        if(TouchingGround() == true)
        {   //While touching ground and holding a direction opposite to where your velocity is going, add drag.
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections) 
            {
                rigidbody2d.drag = linearDrag;
            } 
            else 
            {
                rigidbody2d.drag = 0f;
            }
            rigidbody2d.gravityScale = 0;
        }
        else
        {
            rigidbody2d.gravityScale = gravity;
            rigidbody2d.drag = linearDrag * 0.12f;
            if(rigidbody2d.velocity.y < 0)
            {
                rigidbody2d.gravityScale = gravity * fallMultiplier;
            }
            else if(rigidbody2d.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rigidbody2d.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }




    
    // void Damage(){      //Take Damage. Enemies deal 1 damage. Falling off map deals all damage. Brothario only has 2 health at most.
    //     Debug.Log("Took damage");
    //     plyrHealth -= 1;
    //     if (plyrHealth >= 1){
    //             GameOver();
    //         }
    //         else if (plyrHealth <= 0){
    //             Debug.Log("Activated Game Over Method.");
    //             GameOver();
    //         }
    // }

    private IEnumerator WaitForAnim(){
        yield return new WaitForSeconds(deathDelay);
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
        anim.SetBool("isCrouching", isCrouching);

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

//old jump code
            // //Player jump code block
            // if (Input.GetKey(KeyCode.Space))      //Player will jump if they are both touching the ground and pressing the Jump button
            // {
            //     isTouchingGround = false;
            //     //Player run+jump code block
            //     if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space))
            //     {
            //         rigidbody2d.AddForce(new Vector2(horiz * (runSpeed * 0.4f), jumpForce * 1.2f));                 
            //     }
            //     else if (isTouchingGround = false && Input.GetAxis("Horizontal") != 0)     
            //     {
            //         rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x * (1 + horiz * 10), rigidbody2d.velocity.y));
            //     }
            //     else if (Input.GetKeyUp(KeyCode.LeftShift))     
            //     {
            //         runSpeed = walkSpeed;     
            //     }
            //     else
            //     {
            //         rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x - (horiz), jumpForce));
            //         isTouchingGround = false;
            //     }
            // }