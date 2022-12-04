using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public TextMeshProUGUI coinText; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI worldText;

    //========================= Player Score References ============================
    [SerializeField] int plyrLives = 3;
    private int plyrCoins;
    private int plyrScore;
    //========================= PlayerController Reference ============================
    private PlayerController pc;
    private Rigidbody2D rb2d;
    private Collider2D coll2d;


    //========================= Methods ============================
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            Debug.LogError("Game Manager is Null");

            return _instance;
        }

    }


    private void Awake() {
        _instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        plyrCoins = 00;
        UpdateCoin(0);
        UpdateScore(0);
        UpdateWorldText();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateWorldText()
    {
        int s = SceneManager.GetActiveScene().buildIndex;
        worldText.text = "Level\n" + s;
    }

    public void UpdateScore(int score)
    {
        plyrScore += score;
        scoreText.text = "Brothario\n" + plyrScore;
    }
    public void UpdateCoin(int addCoin)
    {
        plyrCoins += addCoin;
        coinText.text = "x" + plyrCoins;
    }

    public void AddLives(int addLives)
    {
        plyrLives += addLives;
    }

    public void LoseLives(int loseLives)
    {
        plyrLives -= loseLives;
    }

    public void GameOver()
    {
        rb2d.velocity = new Vector2(0, 0);
        coll2d.enabled = false;
        

        if (plyrLives <= 0)
        {
            Debug.Log("GAME OVER.");
            SceneManager.LoadScene("GameOver");
        }
        else if (plyrLives >= 1) 
        {
            Debug.Log("Death occurs. Restarting Level...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //Make sure level name is consistent with Scene level names
        }
    }

    // public void GameOver(bool flag){
    //     _isGameOver = flag;
    // }


    // void GameOver()
    // {
    //     StartCoroutine(WaitForAnim());
    //     rigidbody2d.velocity = new Vector2(0, 0);
    //     collider2d.enabled = false;
        

    //     if (plyrLives <= 0)
    //     {
    //         Debug.Log("GAME OVER.");
    //         SceneManager.LoadScene("GameOver");
    //     }
    //     else {
    //         Debug.Log("Death occurs. Restarting Level...");
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //Make sure level name is consistent with Scene level names
    //     }
    // }




}
