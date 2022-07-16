using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RP : MonoBehaviour
{


    public static bool GameIsPaused = false;
    public GameObject PauseUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume()

    {
        GameIsPaused = false;
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }


    public void Pause()
    {
        GameIsPaused = true;
        Time.timeScale = 0f;
        PauseUI.SetActive(true);
    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
       Application.Quit();
    }



}
