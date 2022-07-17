using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void Play()
    {

        SceneManager.LoadScene("Level1");
    }

    public void ControlM()
    {

        SceneManager.LoadScene("Controls");
    }


    public void Credits()
    {

        SceneManager.LoadScene("Credits");
    }


    public void Quit()
    {
        Application.Quit();
    }

}
