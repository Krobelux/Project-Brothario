using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 10.4f;
    [SerializeField] private float jumpForce = 10f;

    //isTouchingGround components
    [SerializeField] private float grndCheckRadius = 0.24f;
    [SerializeField] private Transform grndCheckPos;
    [SerializeField] private LayerMask whatIsGrnd;
    [SerializeField] private LayerMask whatIsHzrd;
    [SerializeField] private Transform blockCheckPos;
    [SerializeField] private LayerMask whatIsBlock;
    

    private Rigidbody2D rigidbody2d;
    private Collider2D collider2d;
    private bool isTouchingGround = false;
    private bool isTouchingHazard = false;
    private bool isPunchingBlock = false;

    [SerializeField] private float deathDelay = 3.0f;

    //Animation Variable
    Animator anim;

    //Flip Method Variable
    bool isFacingRight;

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
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //2D Platformer so only need to get the Horizontal Input
        
        isTouchingGround = TouchingGround();
        isTouchingHazard = TouchingHazard();
        isPunchingBlock = TouchingBlock();
        float horiz = Input.GetAxis("Horizontal");
        rigidbody2d.velocity = new Vector2(horiz * speed, rigidbody2d.velocity.y);

        //Jump (Bool is faster to compute, so compare a bool arg first before comparison)
        if (TouchingGround() && Input.GetAxis("Jump") > 0)
        {
            rigidbody2d.AddForce(new Vector2(0.0f, jumpForce));
            isTouchingGround = false;


        }

        if (TouchingHazard() == true){
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

    //Overlap Component to check if Player overlaps with hazard
    private bool TouchingHazard(){
        //Debug.Log("is touching hazard");
        return Physics2D.OverlapCircle(grndCheckPos.position, grndCheckRadius, whatIsHzrd);
            
    }

    //Overlap Component to check if Player overlaps block from underneath
    private bool TouchingBlock(){
        Debug.Log("is touching block");
        return Physics2D.OverlapCircle(blockCheckPos.position, grndCheckRadius, whatIsBlock);
            
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
        anim.SetBool("isTouchingBlock", isPunchingBlock);

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