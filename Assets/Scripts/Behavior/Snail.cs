using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    [SerializeField] private Transform headCheckPos;
    [SerializeField] private float headCheckRadius = 0.24f;
    [SerializeField] private LayerMask whatIsPlyr;

    private KillPlayer kp;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate()
    {
        if (HitPlayer() == true)       //Add what to do when Snail hits player in this code block
        {   
            Debug.Log("Touched Player.");
            kp.DmgPlayer(1);
            //kp.DefPlayer();
        }
    }
    


    private bool HitPlayer(){      //Overlap Component to check if Player overlaps with hazard
        
        return Physics2D.OverlapCircle(headCheckPos.position, headCheckRadius, whatIsPlyr);       
    }
}
