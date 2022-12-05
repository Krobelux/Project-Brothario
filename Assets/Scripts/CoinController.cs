using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    [SerializeField] public static int plyrCoins = 0;     //Amount of coins the player has
    PlayerController pc;        //reference to PlayerController
    private GameManager gameManager;
    Animator anim;

    private bool plyrCollectCoin = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        Animation();
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player"){
            gameManager.UpdateCoin(1);
            gameManager.UpdateScore(50);
            Debug.Log("Static Coin Collected.");
            plyrCollectCoin = true;
            Destroy(gameObject, 0.4f);
        }
    }


    void Animation(){
        anim.SetBool("plyrCollect", plyrCollectCoin);
    }


}
