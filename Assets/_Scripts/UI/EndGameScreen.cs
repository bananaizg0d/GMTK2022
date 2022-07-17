using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] TimerScript timer;
    [SerializeField] TextMeshProUGUI textObj;

    void OnEnable()
    {
        textObj.text = timer.finalTimerValue;
        FindObjectOfType<DiceScript>().canRoll = false;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
