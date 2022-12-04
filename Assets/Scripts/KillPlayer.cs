using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    //This script will be called when you want to deal damage to the player.
    //It should check the damage the player takes (1 usually) and will send GameManager.GameOver()
    //which will be handled by GameManager.cs


    static int pcHealth;
    PlayerController pc;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        pcHealth = pc.plyrHealth;
        gm.GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        if (pcHealth <= 0)
        {
            DefPlayer();
        }
        
    }

    public void DefPlayer()
    {
        gm.LoseLives(1);
        gm.GameOver();
    }

    public void DmgPlayer(int dmg)
    {
        pcHealth -= dmg;
    }
}
