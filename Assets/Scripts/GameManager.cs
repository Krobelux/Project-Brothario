using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            Debug.LogError("Game Manager is Null");

            return _instance;
        }

    }

    private int plyrCoins;
    public TextMeshProUGUI coinText;


    private void Awake() {
        _instance = this;
    }

    // public void GameOver(bool flag){
    //     _isGameOver = flag;
    // }

    // Start is called before the first frame update
    void Start()
    {
        plyrCoins = 0;
        UpdateCoin(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateCoin(int addCoin)
    {
        plyrCoins += addCoin;
        coinText.text = "Score: " + plyrCoins;
    }

}
