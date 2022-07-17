using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOonEnable : MonoBehaviour
{

    void OnEnable()
    {
        FindObjectOfType<DiceScript>().canRoll = false;
    }

/*    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}
