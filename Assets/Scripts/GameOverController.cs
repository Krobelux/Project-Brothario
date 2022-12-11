using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameManager gameManager;
    int lastLevel;

    public void Awake() {
        gameManager = GetComponent<GameManager>();
        lastLevel = GameManager.GetCurrentLevel();
        Debug.Log("Last level index: " + lastLevel);
    }
    public void GameStart()
    {
        //Debug.Log("Last level index: " + lastLevel);
        
        //SceneManager.LoadScene("Level 1");    //For restarting the game completely from Level 1
        SceneManager.LoadScene(lastLevel);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
