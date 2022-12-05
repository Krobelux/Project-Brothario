using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    GameManager gameManager;




    //Assign properties in Awake()
    //Assign components in Start()
    void Awake()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Achieved Game Over!");
            // gameManager.GameOver();     //NullReferenceException
        }

    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Achieved Game Over!");
    //         gm.GameOver();
    //     }
    // }

}
