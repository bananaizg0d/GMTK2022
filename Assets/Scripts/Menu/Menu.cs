using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void Control()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Controls");
    }


    public void Credits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }


    public void Quit()
    {
        Application.Quit();
    }

}
