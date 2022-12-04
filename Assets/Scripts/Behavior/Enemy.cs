using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Enemy Stats
    [SerializeField] float moveSpeed = 5;
    private float directX = -1f;
    private Rigidbody2D rb2d;
    public Animator anim;
    private Vector3 localScale;
    bool isFacingRight; 
 


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Obstacles"))
        {
            directX *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(directX * moveSpeed, rb2d.velocity.y);
    }

    void LateUpdate() 
    {
        FaceTowards();
    }

    public virtual void FaceTowards()
    {
        if (directX < 0f)
        {
            isFacingRight = true;
        }
        else if (directX > 0f)
        {
            isFacingRight = false;
        }

        if (((isFacingRight == true) && (localScale.x < 0)) || ((isFacingRight == false) && (localScale.x > 0)))
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;

    }


    



}
