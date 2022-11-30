using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Enemy Stats
    [SerializeField] float enemySpeed = 5;
    [SerializeField] public Transform[] waypoints;
    
    protected Vector3 moveTarget;
    protected Vector3 velocity;
    protected Vector3 lastPos;
    
    public Animator anim;
    bool isFacingRight; 
 


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        moveTarget = waypoints[1].position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public virtual IEnumerator SetTarget(Vector3 position)
    {
        yield return new WaitForSeconds(5f);
        moveTarget = position;
        FaceTowards(position - transform.position);
    }

    public virtual void FaceTowards(Vector3 direction)
    {
        if (direction.x < 0f)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else 
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }

    public virtual void Movement()
    {
        velocity = ((transform.position - lastPos) / Time.deltaTime);
        lastPos = transform.position;

        anim.SetFloat("speed", velocity.magnitude);

        if (transform.position != waypoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, enemySpeed * Time.deltaTime);
        }
        else
        {
            if (moveTarget == waypoints[0].position)
            {
                if (isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    StartCoroutine("SetTarget", waypoints[1].position);
                }
            }
            else
            {
                if (!isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    StartCoroutine("SetTarget", waypoints[0].position);
                }
            }
        }
    }
}
