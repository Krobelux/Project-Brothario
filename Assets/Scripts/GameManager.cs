using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //========================= TextMeshPro References ============================
    private static GameManager _instance;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI worldText;
    public TextMeshProUGUI livesText;

    //========================= Player Progress References ============================
    [SerializeField] static int plyrLives = 3;
    private static int plyrCoins = 0;
    private static int plyrScore = 0000;
    private static int currLevel;
    private static bool plyrDead = false;
    //========================= PlayerController References ============================
    private PlayerController pc;
    private Rigidbody2D rb2d;
    private Collider2D coll2d;

    //========================= Audio Reference ============================
    public AudioSource addLifeSfx;

    //========================= Methods ============================
    public GameManager Instance
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
        plyrDead = false;
        UpdateCoin();
        UpdateScore();
        UpdateWorldText();
        UpdateLives();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Coins2Lives();
    }

    public void UpdateWorldText()
    {
        int s = SceneManager.GetActiveScene().buildIndex;
        worldText.text = "Level\n" + s;
    }

    public void UpdateScore()
    {
        scoreText.text = "Brothario\n" + "SCORE: " + plyrScore;
    }
    public void UpdateScore(int score)      //Overload 1
    {
        plyrScore += score;
        scoreText.text = "Brothario\n" + "SCORE: " + plyrScore;
    }

    public void UpdateCoin()
    {
        coinText.text = "x" + plyrCoins;
    }
    public void UpdateCoin(int addCoin)     //Overload 1
    {
        plyrCoins += addCoin;
        coinText.text = "x" + plyrCoins;
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + plyrLives;
    }
    public void AddLives(int addLives)
    {
        plyrLives += addLives;
        livesText.text = "Lives: " + plyrLives;
    }

    public void LoseLives(int loseLives)
    {
        plyrLives -= loseLives;
        livesText.text = "Lives: " + plyrLives;
    }

    public void Coins2Lives()
    {
        if (plyrCoins >= 50)
        {
            addLifeSfx.Play();
            UpdateCoin(-50);
            AddLives(1);
        }
    }

    public void GameOver()
    {
        GetCurrentLevel();
        plyrDead = true;
        plyrLives -= 1;
        
        

        if (plyrLives <= 0)
        {
            Debug.Log("GAME OVER. Current Level: " + currLevel);
            plyrLives += 3;
            SceneManager.LoadScene("GameOver");
        }
        else if (plyrLives >= 1) 
        {
            Debug.Log("Death occurs. Restarting Level... Current Level: " + currLevel);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  //Make sure level name is consistent with Scene level names
        }
    }

    public static int GetCurrentLevel()
    {
        if (!plyrDead)
        {
            currLevel = SceneManager.GetActiveScene().buildIndex;
        }
        
        return currLevel;
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
