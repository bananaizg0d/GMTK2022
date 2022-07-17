using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;
    public void Play()
    {

        SceneManager.LoadScene("Level1");
    }

    public void ControlM()
    {

        canvas1.SetActive(true);
    }


    public void Credits()
    {

        canvas2.SetActive(true);
    }



    public void disablecanvas1()
    {
        canvas1.SetActive(false);

    }

    public void disablecanvas2()
    {
        canvas2.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
