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
    public TextMeshProUGUI livesText;

    //========================= Player Score References ============================
    [SerializeField] static int plyrLives = 3;
    private int plyrCoins;
    private int plyrScore;
    //========================= PlayerController Reference ============================
    private PlayerController pc;
    private Rigidbody2D rb2d;
    private Collider2D coll2d;
    private Vector2 vector2;



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

        rb2d = GetComponent<Rigidbody2D>();
        coll2d = GetComponent<Collider2D>();
        vector2 = new Vector2(0, 0);

        
    }



    // Start is called before the first frame update
    void Start()
    {
        plyrCoins = 00;
        UpdateCoin(0);
        UpdateScore(0);
        UpdateWorldText();
        UpdateLives();

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
        scoreText.text = "Brothario\n" + "SCORE: " + plyrScore;
    }
    public void UpdateCoin(int addCoin)
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

    public void GameOver()
    {
        
        // rb2d.velocity = vector2;
        // coll2d.enabled = false;
        plyrLives -= 1;

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
