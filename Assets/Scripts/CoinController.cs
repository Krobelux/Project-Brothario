using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinController : MonoBehaviour
{
    [SerializeField] public static int plyrCoins = 0;     //Amount of coins the player has
    PlayerController pc;        //reference to PlayerController
    Animator anim;

    private bool plyrCollectCoin = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Animation();
    }

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player"){
            plyrCoins++;
            Debug.Log("Static Coin Collected. Current coins: " + plyrCoins);
            plyrCollectCoin = true;
            Destroy(gameObject, 0.4f);
        }
    }


    void Animation(){
        anim.SetBool("plyrCollect", plyrCollectCoin);
    }


}
