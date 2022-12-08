using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Scene Index Value Definitions
    //Scene Index 0: MainMenu
    //Scene Index 1: Level 1
    //Scene Index 0: Level 2
    //Scene Index 0: GameOver

    public AudioSource mainMenubgm;
    public AudioSource level1bgm;
    public AudioSource level2bgm;
    public AudioSource gameOverbgm;
    // Start is called before the first frame update
    void Start()
    {
        LevelBGM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LevelBGM()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;

        // if (sceneIndexNumber == 0)
        // {
        //     //Play Main Menu music
        // }
        switch(activeScene)
        {
            case 0:     //Play Main Menu music
            mainMenubgm.Play();
                break;

            case 1:     //Play Level 1 music
            level1bgm.Play();
                break;

            case 2:     //Play Level 2 music
            level2bgm.Play();
                break;

            case 3:     //Play Game Over music
            gameOverbgm.Play();
                break;

            default:    //Play Default music
            mainMenubgm.Play();
                break;
        }
    }
}
