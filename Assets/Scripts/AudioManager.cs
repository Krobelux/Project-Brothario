using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    //Scene Index Value Definitions
    //Scene Index 0: MainMenu
    //Scene Index 1: Level 1
    //Scene Index 2: Level 2
    //Scene Index 3: Level 3
    //Scene Index 4: GameOver

    public AudioSource mainMenubgm;
    public AudioSource level1bgm;
    public AudioSource level2bgm;
    public AudioSource level3bgm;
    public AudioSource gameOverbgm;
    // Start is called before the first frame update
    void Start()
    {
        LevelBGM(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelBGM(bool enabled)
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;

        if (enabled == true)
        {
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

                case 3:     //Play Level 3 music
                level3bgm.Play();
                    break;
                
                case 4:     //Play Game Over music
                gameOverbgm.Play();
                    break;

                default:    //Play Default music
                mainMenubgm.Play();
                    break;
            }
        }
        else
        {
                switch(activeScene)
            {
                case 0:     //Stop Main Menu music
                mainMenubgm.Stop();
                    break;

                case 1:     //Stop Level 1 music
                level1bgm.Stop();
                    break;

                case 2:     //Stop Level 2 music
                level2bgm.Stop();
                    break;

                case 3:     //Stop Level 3 music
                level3bgm.Stop();
                    break;

                case 4:     //Stop Game Over music
                gameOverbgm.Stop();
                break;

                default:    //Stop Default music
                mainMenubgm.Stop();
                    break;
            }
        }

        
    }
}
